// Copyright 2006 Alp Toker <alp@atoker.com>
// This software is made available under the MIT License
// See COPYING for details

using System;

//namespace org.freedesktop.DBus
namespace org.freedesktop.DBus
{
	public class DBusException : ApplicationException
	{
	}

	[Flags]
	public enum NameFlag : uint
	{
		None = 0,
		AllowReplacement = 0x1,
		ReplaceExisting = 0x2,
		DoNotQueue = 0x4,
	}

	public enum NameReply : uint
	{
		PrimaryOwner = 1,
		InQueue,
		Exists,
		AlreadyOwner,
	}

	public enum ReleaseNameReply : uint
	{
		Released = 1,
		NonExistent,
		NotOwner,
	}

	public enum StartReply : uint
	{
		//The service was successfully started.
		Success = 1,
		//A connection already owns the given name.
		AlreadyRunning,
	}

	public delegate void NameOwnerChangedHandler (string name, string old_owner, string new_owner);
	public delegate void NameAcquiredHandler (string name);
	public delegate void NameLostHandler (string name);

	[Interface ("org.freedesktop.DBus.Introspectable")]
	public interface Introspectable
	{
		string Introspect ();
	}

	[Interface ("org.freedesktop.DBus.Properties")]
	public interface Properties
	{
		object this [string propname] {get; set;}

		object Get (string @interface, string propname);
		//void Get (string @interface, string propname, out object value);
		void Set (string @interface, string propname, object value);
	}

	[Interface ("org.freedesktop.DBus")]
	public interface Bus : Introspectable
	{
		NameReply RequestName (string name, NameFlag flags);
		ReleaseNameReply ReleaseName (string name);
		string Hello ();
		string[] ListNames ();
		bool NameHasOwner (string name);
		event NameOwnerChangedHandler NameOwnerChanged;
		event NameLostHandler NameLost;
		event NameAcquiredHandler NameAcquired;
		StartReply StartServiceByName (string name, uint flags);
		string GetNameOwner (string name);
		uint GetConnectionUnixUser (string connection_name);
		void AddMatch (string rule);
		void RemoveMatch (string rule);

		//undocumented in spec
		void ReloadConfig ();
	}

	//Having this as an attribute is a bit silly, no?
	[AttributeUsage (AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public class InterfaceAttribute : Attribute
	{
		public string Name;

		public InterfaceAttribute (string name)
		{
			this.Name = name;
		}
	}
}
