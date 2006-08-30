// Copyright 2006 Alp Toker <alp@atoker.com>
// This software is made available under the MIT License
// See COPYING for details

using System;
using System.Collections.Generic;

using org.freedesktop.DBus;

// Hand-written interfaces for bootstrapping

namespace org.freedesktop
{
	/*
	public struct ServerInformation
	{
		public string Name;
		public string Vendor;
		public string Version;
		public string SpecVersion;
	}
	*/

	[Interface ("org.freedesktop.Notifications")]
	public interface Notifications : Introspectable, Properties
	{
		//ServerInformation ServerInformation { get; }

		void GetServerInformation (out string name, out string vendor, out string version, out string spec_version);
		string[] GetCapabilities ();
		void CloseNotification (uint id);
		uint Notify (string app_name, uint id, string icon, string summary, string body, string[] actions, IDictionary<string,object> hints, int timeout);
	}
}
