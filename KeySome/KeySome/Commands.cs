﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;

namespace KeySome;

public class Commands
{
    private static readonly Regex _bindingRegex = new(@"(Ctrl|Alt|Shift|F[1-9]|F1[0-2])");

    public static async Task<IEnumerable<KeyItem>> GetCommandsAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        List<KeyItem> items = new();
        DTE2 dte = await VS.GetServiceAsync<DTE, DTE2>();

        foreach (Command command in dte.Commands)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                continue;
            }

            if (command.Bindings is object[] bindings && bindings.Length > 0)
            {
                foreach (object bindingObj in bindings)
                {
                    string binding = bindingObj.ToString();
                    if (_bindingRegex.IsMatch(binding))
                    {
                        int scopeIndex = binding.IndexOf("::");
                        string scope = "";

                        if (scopeIndex >= 0)
                        {
                            scope = binding.Substring(0, scopeIndex);
                            binding = binding.Substring(scopeIndex + 2);
                        }
                        else
                        {
                            
                        }

                        if (scope.Equals("Unknown Editor", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        int index = command.Name.IndexOf('.');
                        string prefix = index > 0 ? CleanName(command.Name.Substring(0, index)) : "Misc";

                        items.Add(new KeyItem(command.Name, binding, prefix, scope));
                    }
                    else
                    {

                    }
                }
            }
        }

        return items.OrderBy(i => i.Category);
    }

    public static string CleanName(string name)
    {
        StringBuilder sb = new();
        sb.Append(name[0]);

        for (int i = 1; i < name.Length; i++)
        {
            char c = name[i];

            if (char.IsUpper(c) && !char.IsUpper(name[i - 1]))
            {
                sb.Append(" ");
            }

            sb.Append(c);
        }

        return sb.ToString();
    }
}
