using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace ByteLibrary.ActiveDirectory
{
    public class Group : DirectoryEntry
    {
        private static readonly string objectClass = "group";
        private static readonly string objectCategory = "group";

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

        public PropertyValueCollection Members
        {
            get { return this.Properties["member"] as PropertyValueCollection; }
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

        public ActiveDs.ADS_GROUP_TYPE_ENUM GroupType
        {
            get
            {
                return (ActiveDs.ADS_GROUP_TYPE_ENUM)Enum.ToObject(
                    typeof(ActiveDs.ADS_GROUP_TYPE_ENUM), (int)this.Properties["groupType"].Value);
            }
            set
            {
                this.Properties["groupType"].Value = value;
            }
        }

        public Group(string path) : base(path) { }

        public new void Rename(string newName)
        {
            base.Rename(string.Format("CN={0}", newName));
            
            this.Properties["displayName"].Value = newName;
            
            this.CommitChanges();
            this.RefreshCache();
        }

        public void AddChild(string DN)
        {
            this.Properties["member"].Add(DN);
            
            this.CommitChanges();
            this.RefreshCache();
        }

        public void RemoveChild(string DN)
        {
            this.Properties["member"].Remove(DN);

            this.CommitChanges();
            this.RefreshCache();
        }

        public static DirectoryEntry Create(DirectoryEntry parent, string name, ActiveDs.ADS_GROUP_TYPE_ENUM type)
        {
            DirectoryEntry created = parent.Children.Add(name, objectClass);
            created.Properties["groupType"].Value = (int)created.Properties["groupType"].Value | (int)type;
            
            created.CommitChanges();
            created.RefreshCache();
            
            return created;
        }

        public static DirectoryEntry Find(DirectoryEntry root, string sAMAccountName)
        {
            string queryString = string.Format("(&(sAMAccountName={0})(objectClass={1}))", sAMAccountName, objectClass);
            IEnumerable<string> paths = Query.RunQuery(root, queryString, SearchScope.Subtree);
            return new DirectoryEntry(paths.First());
        }

        public static IEnumerable<string> FindActive(string domainController)
        {
            int normalUser = (int)ActiveDs.ADS_USER_FLAG.ADS_UF_NORMAL_ACCOUNT;
            int active = (int)ActiveDs.ADS_USER_FLAG.ADS_UF_ACCOUNTDISABLE;

            using (var root = new DirectoryEntry(string.Format("LDAP://{0}", domainController)))
            {
                //AD query brain explosion
                string filter = string.Format(
                    "(&((objectClass={0})(objectCategory={1})(userAccountControl:1.2.840.113556.1.4.803:={2})(!(userAccountControl:1.2.840.113556.1.4.803:={3}))(!(cn=*$))))", 
                    objectClass, 
                    objectCategory, 
                    normalUser, 
                    active);

                return Query.RunQuery(root, filter, SearchScope.Subtree);
            }
        }

        public static IEnumerable<string> FindAll(string domainController)
        {
            using (var root = new DirectoryEntry(string.Format("LDAP://{0}", domainController)))
            {
                string filter = string.Format("(&(objectClass={0})(objectCategory={1}))", objectClass, objectCategory);
                return Query.RunQuery(root, filter, SearchScope.Subtree);
            }
        }

        public static void Delete(DirectoryEntry root, string sAMAccountName)
        {
            using (DirectoryEntry entry = Find(root, sAMAccountName))
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
