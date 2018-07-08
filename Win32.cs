/**
 * Copyright (c) 2017 Daniel Lo Nigro (Daniel15)
 * 
 * This source code is licensed under the MIT license found in the 
 * LICENSE file in the root directory of this source tree. 
 */

using System.Runtime.InteropServices;

namespace RetroArchNoConsole
{
	public static class Win32
	{
		[DllImport("kernel32.dll")]
		public static extern bool AttachConsole(int dwProcessId);
		public const int ATTACH_PARENT_PROCESS = -1;
	}
}
