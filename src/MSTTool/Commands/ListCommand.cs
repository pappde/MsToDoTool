﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using Todo.MSTTool.Commands;
using Todo.Core.Model;

namespace MSTTool.Commands
{
    public class ListCommand : TargetListCommandBase
    {
        public ListCommand(IServiceProvider serviceProvider) : base("list", serviceProvider)
        {
            Description = "Retrieves a list of the ToDo items.";
        }

        // TODO: drp033123 - we don't need targetFolder,
        public override async Task RunCommandAsync(TodoList list)
        {
            var tasksAsync = Repo.GetListTasksAsyncEnumerable(list);

            int tasksCount = 0;
            await foreach (var task in tasksAsync)
            {
                // TODO: drp033123 - include checkmark - see original project
                Console.WriteLine("[{2}] {0}/{1} [{3}]", list.displayName, task.title, task.status, task.id);
                tasksCount++;
            }

            Console.WriteLine("\tTotal: {0} tasks", tasksCount);
        }

    }
}