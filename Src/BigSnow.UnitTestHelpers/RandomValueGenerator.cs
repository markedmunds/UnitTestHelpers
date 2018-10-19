using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSnow.UnitTestHelpers
{
	public sealed class RandomValueGenerator
	{
		#region Thread-safe, lazy Singleton
		/// <summary>
		/// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
		/// for more details about its implementation.
		/// </summary>
		public static RandomValueGenerator Instance
		{
			get
			{
				return Nested.RandomValueGenerator;
			}
		}

		/// <summary>
		/// Private constructor to enforce singleton
		/// </summary>
		private RandomValueGenerator() { }

		/// <summary>
		/// Assists with ensuring thread-safe, lazy singleton
		/// </summary>
		private class Nested
		{
			static Nested() { }
			internal static readonly RandomValueGenerator RandomValueGenerator = new RandomValueGenerator();
		}
		#endregion

		private readonly Random _rand = new Random();
		public int GetRandomInteger()
		{
			return _rand.Next();
		}

		public int GetRandomInteger(int min, int max)
		{
			return _rand.Next(min, max);
		}

		public long GetRandomLong()
		{
			return Convert.ToInt64((long.MaxValue * 1.0 - 0 * 1.0) * _rand.NextDouble() + 0 * 1.0);
		}

		public double GetRandomDouble()
		{
			return _rand.NextDouble();
		}

		public string GetRandomString(int size)
		{
			return GetRandomString(size, false);
		}

		public string GetRandomString(int size, bool lowerCase)
		{
			StringBuilder builder = new StringBuilder();
			char ch;
			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rand.NextDouble() + 65)));
				builder.Append(ch);
			}
			if (lowerCase)
				return builder.ToString().ToLower();
			return builder.ToString();
		}

		public bool GetRandomBoolean()
		{
			return Convert.ToBoolean(_rand.Next(0, 2));
		}
	}
}
