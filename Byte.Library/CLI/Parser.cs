using System;
using System.Collections.Generic;
using System.Linq;

namespace Byte.Library.CLI
{
    public class Parser
    {
        private Options options;
        private IEnumerable<string> optionalArguments;
        private IEnumerable<string> requiredArguments;
        public IDictionary<string, string> Parsed { get; private set; }

        public Parser(Options options, IEnumerable<string> optionalArguments, IEnumerable<string> requiredArguments)
        {
            this.options = options;

            if (optionalArguments == null)
            {
                this.optionalArguments = new List<string>();
            }
            else
            {
                this.optionalArguments = optionalArguments;
            }

            if (requiredArguments == null)
            {
                this.requiredArguments = new List<string>();
            }
            else
            {
                this.requiredArguments = requiredArguments;
            }

            this.Parsed = new Dictionary<string, string>();
        }

        public void Parse(string arguments)
        {
            this.Parse(arguments.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Parse(IEnumerable<string> arguments)
        {
            this.Parsed.Clear();
            this.ProcessTokens(this.Tokenize(arguments));
            this.ValidateRequirementsSatisfied();
        }

        private void ProcessTokens(List<string> tokens)
        {
            var expectedArguments = new List<string>();
            expectedArguments.AddRange(this.requiredArguments);
            expectedArguments.AddRange(this.optionalArguments);

            int i = 0;
            while (i != tokens.Count)
            {
                string thisToken = tokens[i];

                if (!expectedArguments.Contains(thisToken))
                {
                    i++;
                }
                else
                {
                    string nextToken = String.Empty;

                    if (i == tokens.Count - 1)
                    {
                        this.Parsed.Add(thisToken, String.Empty);
                        i++;
                    }
                    else
                    {
                        nextToken = tokens[i + 1];

                        if (expectedArguments.Contains(thisToken) && !expectedArguments.Contains(nextToken))
                        {
                            this.Parsed.Add(thisToken, nextToken);
                            i += 2;
                        }
                        else if (expectedArguments.Contains(thisToken) && expectedArguments.Contains(nextToken))
                        {
                            this.Parsed.Add(thisToken, String.Empty);
                            i++;
                        }
                    }
                }
            }
        }

        private List<string> Tokenize(IEnumerable<string> arguments)
        {
            var tokens = new List<string>();

            foreach (var argument in arguments)
            {
                string trimmed = argument.Trim().TrimStart('-');

                if (trimmed.Contains('='))
                {
                    string[] parts = trimmed.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0)
                    {
                        continue;
                    }
                    else if (parts.Length == 1)
                    {
                        tokens.Add(parts[0]);
                        tokens.Add(String.Empty);
                    }
                    else
                    {
                        tokens.Add(parts[0]);
                        tokens.Add(parts[1]);
                    }
                }
                else
                {
                    tokens.Add(trimmed);
                }
            }

            return tokens;
        }

        private void ValidateRequirementsSatisfied()
        {
            foreach (var requiredArgument in this.requiredArguments)
            {
                bool requiredFound = false;

                if(!this.options.HasFlag(Options.CaseSensitive))
                {
                    requiredFound = this.Parsed.Keys.Contains(requiredArgument, StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    requiredFound = this.Parsed.Keys.Contains(requiredArgument);
                }

                if (!requiredFound)
                {
                    throw new ArgumentException(string.Format("'{0}' specified as required but not found.", requiredArgument));
                }
            }
        }

        public bool IsSet(string key)
        {
            return this.Parsed.ContainsKey(key);
        }

        public string Get(string key)
        {
            return this.Parsed[key];
        }
    }
}
