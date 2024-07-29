﻿using PhalconSoft.Helpers;

namespace PhalconSoft.Services
{
	public class ExampleErrorService
	{
		public void ExampleErrors()
		{
			// a custom app exception that will return a 400 response
			throw new AppException("Email or password is incorrect");

			// a key not found exception that will return a 404 response
			throw new KeyNotFoundException("Account not found");
		}
	}
}
