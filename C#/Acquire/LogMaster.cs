using System.Collections.Generic;
using System.Windows.Forms;

namespace Acquire
{
    public class LogMaster
    {
        #region Public Variables

        /// <summary>
        /// The array holding the singular and plural forms of "share"
        /// </summary>
        public static readonly string[] SHARES = {"share", "shares"};

        /// <summary>
        /// The array holding the number progression for sentence structure.
        /// 30 is the max because companies can only have up to 30 shares.
        /// </summary>
        public static readonly string[] STANDARD_NUPROG = {"a", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"};

        /// <summary>
        /// Indicates a normal log message with no logic
        /// </summary>
        public const string NORMAL_TYPE = "Norm";
        /// <summary>
        /// Indicates a dynamic log message which replaces the previous message
        /// </summary>
        public const string DYNAMIC_TYPE = "Dyn";
        /// <summary>
        /// Indicates a dynamic log message with specific logic regarding company shares
        /// </summary>
        public const string SHARES_TYPE = "ShareS";

        #endregion

        #region Private Member Variables

        // The text box that this class controls
        private static ListBox listBox;

        // The last string logged
        private static string lastLog = "";

        // The type of the last string
        private static string lastDynamicType = "";

        // The type of the last string logged
        private static string lastLogType = "";

        // The last dynamic strings
        private static string[] lastDynamic = new string[2];

        // The current numbers for dynmic logging
        private static readonly Dictionary<string, int> STORED_NUMBERS = new Dictionary<string, int>();

        // The current strings for dynmic logging
        private static readonly Dictionary<string, string[]> STORED_STRINGS = new Dictionary<string, string[]>();

        // Whether or not the last string is being replaced
        private static bool isBeingReplaced;

        #endregion

        #region Action Methods

        /// <summary>
        /// Logs the given <paramref name="log"/> if it hasn't been logged already.
        /// <para>
        /// Scrolls the ListBox to the very end.
        /// </para>
        /// </summary>
        /// 
        /// <param name="log">The string to be logged</param>
        public static void Log(string log)
        {
            // Don't log if it's been logged already
            if (!log.Equals(lastLog))
            {
                // Add the text to the box
                listBox.Items.Add($"\n> {log}");
                // SCroll to the bottom
                listBox.TopIndex = listBox.Items.Count - listBox.Height / listBox.ItemHeight;
                // Save the string for later
                lastLog = log;
                // Save the type for later
                lastLogType = NORMAL_TYPE;
            }
        }

        /// <summary>
        /// Logs content dynmically. If <see cref="isBeingReplaced"/> is true, it replaces the last line with the new line.
        /// </summary>
        /// 
        /// <param name="log">The string to be logged</param>
        public static void DynamicLog(string log)
        {
            // If we are replacing the last line and it was dynamic
            if (isBeingReplaced && lastLogType.Equals(DYNAMIC_TYPE))
            {
                listBox.Items.RemoveAt(listBox.Items.Count - 1);
                isBeingReplaced = false;
            }

            // Add the text to the box
            listBox.Items.Add($"\n> {log}");
            // SCroll to the bottom
            listBox.TopIndex = listBox.Items.Count - listBox.Height / listBox.ItemHeight;
            // Save the string for later
            lastLog = log;
            // Save the type for later
            lastLogType = DYNAMIC_TYPE;
        }

        /// <summary>
        /// Creates dynamic content based on the given key, start position, increment size, and type
        /// </summary>
        /// 
        /// <param name="key">The key used when storing and incrementing</param>
        /// <param name="start">Where to start from</param>
        /// <param name="increment">How much to increment by</param>
        /// <param name="type">The type of this content</param>
        /// 
        /// <returns>A dynamically constructed string</returns>
        public static string DynamicContent(string key, int start, int increment, string type)
        {
            // The string to output, starts blank.
            string output = "";

            // If we haven't used this key before
            if (!STORED_NUMBERS.ContainsKey(key))
            {
                // Store it
                STORED_NUMBERS.Add(key, start);
                // The starting size is all there is at this point
                output += start;
            }
            // Otherwise
            else
            {
                // If this is about Shares, get the last dynamic shares, otherwise get the other one
                string lastDyn = type.Equals(SHARES_TYPE) ? lastDynamic[0] : lastDynamic[1];

                // If the current key isn't the same as the last dynamic entry, reset it
                if (!key.Equals(lastDyn) || !lastLogType.Equals(DYNAMIC_TYPE))
                {
                    // Start fresh with this new key
                    STORED_NUMBERS.Clear();
                    STORED_NUMBERS.Add(key, start);
                    output += start;
                }
                // Otherwise
                else
                {
                    // Increment the stored count by the given amount
                    STORED_NUMBERS[key] += increment;
                    output += STORED_NUMBERS[key];
                    // Set this up to be replaced in the log
                    isBeingReplaced = true;
                }
            }

            // Store the last used key for the current type
            if (type.Equals(SHARES_TYPE))
            {
                lastDynamic[0] = key;
            }
            else
            {
                lastDynamic[1] = key;
            }

            // If this isn't a shares type, store what it is
            if (!type.Equals(SHARES_TYPE))
            {
                lastDynamicType = type;
            }

            return output;
        }

        /// <summary>
        /// Dynamically constructs a string based on the given key, values, increment size, and type
        /// </summary>
        /// 
        /// <param name="key">The key used when storing and incrementing</param>
        /// <param name="values">The array of values to pull from when creating the string</param>
        /// <param name="increment">How much to increment by</param>
        /// <param name="type">The type of this content</param>
        /// 
        /// <returns>A dynamically constructed string based on the given key, values, increment size, and type</returns>
        public static string DynamicContent(string key, string[] values, int increment, string type)
        {
            // The string to output, starts blank.
            string output = "";

            // If we haven't used this key before
            if (!STORED_STRINGS.ContainsKey(key))
            {
                // Store it
                STORED_STRINGS.Add(key, values);
                // And output the first potential value
                output = values[0];
            }
            else
            {
                // If this is about Shares, get the last dynamic shares, otherwise get the other one
                string lastDyn = type.Equals(SHARES_TYPE) ? lastDynamic[0] : lastDynamic[1];

                // If the current key isn't the same as the last dynamic entry, reset it
                if (!key.Equals(lastDyn) || !lastLogType.Equals(DYNAMIC_TYPE))
                {
                    // Start fresh with this new key
                    STORED_STRINGS.Clear();
                    STORED_STRINGS.Add(key, values);
                    output += values[0];
                }
                else
                {
                    // Adjust the increment size if needed
                    if (STORED_STRINGS[key].GetLength(0) <= increment)
                    {
                        increment = STORED_STRINGS[key].GetLength(0) - 1;
                    }

                    // If we are incrementing at all
                    if (increment > 0)
                    {
                        // Store the current strings
                        string[] arrStrings = new string[STORED_STRINGS[key].GetLength(0) - increment];

                        // Shift them down
                        for (int i = 0; i < arrStrings.GetLength(0); i++)
                        {
                            arrStrings[i] = STORED_STRINGS[key][i + increment];
                        }

                        // Store the shifted strings
                        STORED_STRINGS[key] = arrStrings;
                    }

                    // Output the first string
                    output = STORED_STRINGS[key][0];
                    // Set this up to be replaced in the log
                    isBeingReplaced = true;
                }
            }

            // Store the last used key for the current type
            if (type.Equals(SHARES_TYPE))
            {
                lastDynamic[0] = key;
            }
            else
            {
                lastDynamic[1] = key;
            }

            // If this isn't a shares type, store what it is
            if (!type.Equals(SHARES_TYPE))
            {
                lastDynamicType = type;
            }

            return output;
        }

        /// <summary>
        /// Clears the dynamic content being stored
        /// </summary>
        /// 
        /// <param name="strings">Whether or not to clear the strings</param>
        /// <param name="numbers">Whether or not to clear the numbers</param>
        public static void ClearDynamicContent(bool strings, bool numbers)
        {
            if (strings)
            {
                STORED_STRINGS.Clear();
            }
            if (numbers)
            {
                STORED_NUMBERS.Clear();
            }

            if (strings || numbers)
            {
                lastDynamic = new string[2];
                isBeingReplaced = false;
            }
        }

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets the associated ListBox to listBox
        /// </summary>
        /// 
        /// <param name="listBox">The ListBox to associate with this class</param>
        public static void SetListBox(ListBox listBox)
        {
            LogMaster.listBox = listBox;
        }

        #endregion
    }
}
