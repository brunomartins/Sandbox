using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;

namespace SandboxDynUtilities.Functions
{
    [IsVisibleInDynamoLibrary(false)]
    public class About
    {
        /// <summary>
        /// Method used by the about node
        /// </summary>
        /// <param name="name">A name.</param>
        /// <param name="check">A check value.</param>
        /// <returns>A nice hello with a quote.</returns>
        public static string AboutSandbox(string name, bool check)
        {
            string aboutSandbox = "Sandbox is a set of useful nodes to help speed your day to day work at Mott.";
            string sayHello = "Hello" + name + "\n" + aboutSandbox;

            List<string> quoteList = new List<string>
            {
                "\"Never be limited by other people’s limited imaginations.\" - Dr. Mae Jemison",
                "\"Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome while trying to succeed.\" - Booker T. Washington",
                "\"I have not failed. I’ve just found 10,000 ways that won’t work.\" - Thomas A.Edison",
                "\"If you can dream, you can do it.\" - Walt Disney",
                "\"Where there is a will, there is a way. If there is a chance in a million that you can do something, anything, to keep what you want from ending, do it. Pry the door open or, if need be, wedge your foot in that door and keep it open.\" - Pauline Kael",
                "\"The future belongs to those who believe in the beauty of their dreams.\" - Eleanor Roosevelt",
                "\"You just can’t beat the person who never gives up.\" - Babe Ruth",
                "\"Start where you are. Use what you have. Do what you can.\" - Arthur Ashe",
                "\"If you can't explain it simply, you don't understand it well enough.\" - Albert Einstein",
                "\"No work is ever wasted. If it’s not working, let go and move on – it’ll come back around to be useful later.\" - Pixar"
            };

            Random rnd = new Random();
            int r = rnd.Next(quoteList.Count);

            if (check)
            {
                return sayHello + "\n" + quoteList[r];
            }

            return sayHello;
        }
    }
}
