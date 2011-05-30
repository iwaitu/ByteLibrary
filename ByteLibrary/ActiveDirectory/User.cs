using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace ByteLibrary.ActiveDirectory
{
    public class User : DirectoryEntry
    {
        private static readonly string objectClass = "user";
        private static readonly string objectCategory = "person";

        public string SamAccountName
        {
            get { return this.Properties["sAMAccountName"].Value as string; }
        }

        public string DN
        {
            get { return this.Properties["distinguishedName"].Value as string; }
        }

        public string CN
        {
            get { return this.Properties["CN"].Value as string; }
        }

        public PropertyValueCollection MemberOf
        {
            get { return this.Properties["memberOf"] as PropertyValueCollection; }
        }

        public string DisplayName
        {
            get
            {
                return this.Properties["displayName"].Value as string;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.Properties["displayName"].Value = this.SamAccountName;
                }
                else
                {
                    this.Properties["displayName"].Value = value;
                }

                this.CommitChanges();
                this.RefreshCache();
            }
        }

        public bool IsLocked
        {
            get
            {
                return Convert.ToBoolean(this.InvokeGet("IsAccountLocked"));
            }
            set
            {
                if (value == false)
                {
                    this.Properties["LockOutTime"].Value = 0;
                }

                this.InvokeSet("IsAccountLocked", value);
                
                this.CommitChanges();
                this.RefreshCache();
            }
        }

        public ActiveDs.ADS_USER_FLAG UserAccountControl
        {
            get
            {
                return (ActiveDs.ADS_USER_FLAG)Enum.ToObject(
                    typeof(ActiveDs.ADS_USER_FLAG), (int)this.Properties["userAccountControl"].Value);
            }
            set
            {
                this.Properties["userAccountControl"].Value = value;
            }
        }

        public User(string path) : base(path) { }

        public new void Rename(string newName)
        {
            base.Rename(string.Format("CN={0}", newName));
            Properties["displayName"].Value = newName;

            this.CommitChanges();
            this.RefreshCache();
        }

        public void Enable()
        {
            this.Properties["userAccountControl"].Value = (int)Properties["userAccountControl"].Value & ~0x2;

            this.CommitChanges();
            this.RefreshCache();
        }

        public void Disable()
        {
            this.Properties["userAccountControl"].Value = (int)Properties["userAccountControl"].Value | 0x2;

            this.CommitChanges();
            this.RefreshCache();
        }

        public void SetPassword(string pass)
        {
            this.Invoke("SetPassword", new object[] { pass });
            
            this.CommitChanges();
            this.RefreshCache();
        }

        public static DirectoryEntry Create(DirectoryEntry parent, string name, string pass, ActiveDs.ADS_USER_FLAG userAccountControl)
        {
            DirectoryEntry created = parent.Children.Add(string.Format("CN={0}", name), "user");
            
            created.Properties["sAMAccountName"].Value = name;
            created.Properties["userAccountControl"].Value = (int)created.Properties["userAccountControl"].Value | (int)userAccountControl;
            created.Invoke("SetPassword", new object[] { pass });
            
            created.CommitChanges();
            created.RefreshCache();
            
            return created;
        }

        public static DirectoryEntry Find(string domainController, string sAMAccountName)
        {
            using (var root = new DirectoryEntry(string.Format("LDAP://{0}", domainController)))
            {
                string queryString = string.Format("(&(sAMAccountName={0})(objectClass={1}))", sAMAccountName, objectClass);
                List<string> paths = Query.RunQuery(root, queryString, SearchScope.Subtree);
                return new DirectoryEntry(paths[0]);
            }
        }

        public static List<string> FindActive(string domainController)
        {
            int normalUser = (int)ActiveDs.ADS_USER_FLAG.ADS_UF_NORMAL_ACCOUNT;
            int active = (int)ActiveDs.ADS_USER_FLAG.ADS_UF_ACCOUNTDISABLE;

            using (var root = new DirectoryEntry(string.Format("LDAP://{0}", domainController)))
            {
                string filter = string.Format(
                    "(&((objectClass={0})(objectCategory={1})(userAccountControl:1.2.840.113556.1.4.803:={2})(!(userAccountControl:1.2.840.113556.1.4.803:={3}))(!(cn=*$))))", 
                    objectClass, 
                    objectCategory, 
                    normalUser, 
                    active);

                return Query.RunQuery(root, filter, SearchScope.Subtree);
            }
        }

        public static List<string> FindAll(string domainController)
        {
            using (var root = new DirectoryEntry(string.Format("LDAP://{0}", domainController)))
            {
                string filter = string.Format("(&(objectClass={0})(objectCategory={1}))", objectClass, objectCategory);
                return Query.RunQuery(root, filter, SearchScope.Subtree);
            }
        }

        public static void Delete(string domainController, string sAMAccountName)
        {
            using (DirectoryEntry entry = Find(domainController, sAMAccountName))
            {
                if (entry != null)
                {
                    entry.Parent.Children.Remove(entry);
                    entry.CommitChanges();
                }
            }
        }
    }
}
