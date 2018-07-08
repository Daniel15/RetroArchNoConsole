/**
 * Copyright (c) 2018 Daniel Lo Nigro (Daniel15)
 * 
 * This source code is licensed under the MIT license found in the 
 * LICENSE file in the root directory of this source tree. 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RetroArchNoConsole
{
	public static class Program
	{
		[STAThread]
		public static int Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var retroArchPath = FindRetroArch();
			if (string.IsNullOrEmpty(retroArchPath))
			{
				MessageBox.Show(
					"Could not find RetroArch. Ensure this executable is in the same directory",
					"Could not find RetroArch",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return 1;
			}

			var argsString = string.Join(
				" ",
				// Very basic escaping, but it works well enough for this use case
				args.Select(arg => arg.Contains(' ') ? "\"" + arg + "\"" : arg)
			);

			Win32.AttachConsole(Win32.ATTACH_PARENT_PROCESS);
			Console.WriteLine($"Running: {retroArchPath} {argsString}");

			var process = new Process
			{
				StartInfo =
				{
					FileName = retroArchPath,
					WorkingDirectory = Path.GetDirectoryName(retroArchPath),
					Arguments = argsString,
					WindowStyle = ProcessWindowStyle.Hidden,
				}
			};
			process.Start();
			process.WaitForExit();
			return 0;
		}

		/// <summary>
		/// Determines the path to RetroArch
		/// </summary>
		private static string FindRetroArch()
		{
			var potentialPaths = new[]
			{
				Path.Combine(Directory.GetCurrentDirectory(), "RetroArch.exe"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RetroArch.exe"),
			};
			return potentialPaths.FirstOrDefault(File.Exists);
		}
	}
}
