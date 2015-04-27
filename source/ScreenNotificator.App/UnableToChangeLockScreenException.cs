using System;

namespace ScreenNotificator.App
{
	public class UnableToChangeLockScreenException : Exception
	{
		public UnableToChangeLockScreenException()
			: base("Lock screen image was not changed!")
		{
			
		}
	}
}