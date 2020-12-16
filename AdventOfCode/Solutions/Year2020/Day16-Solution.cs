using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day16 : ASolution
    {
        string partOne;
        string partTwo;

        public Day16() : base(16, 2020, "")
        {
            string[] portions = Input.Split("\n\n");

            // All the fields that appear on this ticket and their valid ranges
            List<TicketField> ticketFields = new List<TicketField>();

            // Set of tickets that don't have any invalid fields
            List<int[]> validTickets = new List<int[]>();
            List<int> pertinentFields = new List<int>();

            // Field count tracker
            int fieldCount = 0;
            
            // Cycle through available fields
            foreach (string line in portions[0].Split("\n"))
            {
                // Split between field name and ranges
                string[] lineParts = line.Split(":");
                if (lineParts[0].StartsWith("departure")) pertinentFields.Add(fieldCount);

                // Get both sets of upper and lower bounds
                string[] numParts = lineParts[1].Split(" or ", StringSplitOptions.TrimEntries);

                string[] boundsOne = numParts[0].Split("-");
                string[] boundsTwo = numParts[1].Split("-");

                // Create the new ticket field
                ticketFields.Add(new TicketField(int.Parse(boundsOne[0]), int.Parse(boundsOne[1]),
                                                 int.Parse(boundsTwo[0]), int.Parse(boundsTwo[1])));
                
                fieldCount++;
            }

            // Read in the values on my ticket
            int[] myTicket = portions[1].Split("\n")[1].ToIntArray(",");

            long invalidSum = 0;

            // Go through each ticket and sum up invalid options
            foreach (string line in portions[2].Split("\n"))
            {
                bool isValid = true;
                int[] ticketValues = line.ToIntArray(",");
                for (int i = 0; i < ticketValues.Length; i++)
                {
                    if (!TicketField.IsValidAny(ticketValues[i]))
                    {
                        invalidSum += ticketValues[i];
                        isValid = false;
                    }
                }

                // Keep track of valid tickets
                if (isValid) validTickets.Add(ticketValues);
            }

            // Save the answer to part one
            partOne = invalidSum.ToString();

            // Keep track of which combinations have been invalidated;
            // First coordinate is the index of the field within the ticket
            // Second coordinate is the index of the field in our list of fields
            bool[,] invalidated = new bool[fieldCount, fieldCount];

            // Go through each valid ticket and invalidate field to ticket index
            // combinations that don't work because they're out of bounds
            foreach (int[] ticketVals in validTickets)
            {
                for (int i = 0; i < ticketVals.Length; i++)
                {
                    for (int j = 0; j < ticketFields.Count; j++)
                    {
                        if (!ticketFields[j].IsValid(ticketVals[i]))
                        {
                            invalidated[i, j] = true;
                        }
                    }
                }
            }

            // This keeps track of the ticket-index
            // of fields that we know for certain
            Dictionary<int, int> fieldToIndex = new Dictionary<int, int>();

            // Loop until we've got a single valid ticket-index for each field
            bool allValidated = false;
            while (!allValidated)
            {
                allValidated = true;

                // Go through each ticket index
                for (int ticketIndex = 0; ticketIndex < fieldCount; ticketIndex++)
                {
                    int validFieldCount = 0;
                    int firstValidField = -1;

                    // Go through each field and see if it's still valid
                    for (int field = 0; field < fieldCount; field++)
                    {
                        if (!invalidated[ticketIndex, field])
                        {
                            validFieldCount++;
                            firstValidField = field;
                        }
                    }

                    // If we only found 1 valid field, that must be the
                    // field that corresponds to this ticket index!
                    if (validFieldCount == 1)
                    {
                        // Update our dictionary
                        fieldToIndex[firstValidField] = ticketIndex;

                        // Invalidate this field for all other ticket indexes
                        for (int otherTicketIndex = 0; otherTicketIndex < fieldCount; otherTicketIndex++)
                        {
                            // Don't invalidate it for ourselves!
                            if (otherTicketIndex != ticketIndex)
                            {
                                invalidated[otherTicketIndex, firstValidField] = true;
                            }
                        }
                    }
                    else
                    {
                        allValidated = false;
                    }
                }
            }

            long product = 1;

            // Multiply together all of the pertinent field values
            foreach (int i in pertinentFields)
            {
                product *= myTicket[fieldToIndex[i]];
            }

            // Save the answer to part two
            partTwo = product.ToString();
        }

        // A ticket field has a set of valid values
        private class TicketField
        {
            // Keep track of *all* valid values
            static HashSet<int> validAny;

            // Keep track of valid values for this object
            HashSet<int> validValues;

            // Object to keep track of valid values for a given field
            public TicketField(int l1, int u1, int l2, int u2)
            {
                validValues = new HashSet<int>();
                if (null == validAny) validAny = new HashSet<int>();

                // Add all valid values to the sets
                for (int i = l1; i <= u1; i++)
                {
                    validValues.Add(i);
                    validAny.Add(i);
                }

                for (int i = l2; i <= u2; i++)
                {
                    validValues.Add(i);
                    validAny.Add(i);
                }
            }

            // Is value valid for this field?
            public bool IsValid(int value)
            {
                return validValues.Contains(value);
            }

            // Is value valid for any field?
            public static bool IsValidAny(int value)
            {
                return validAny.Contains(value);
            }
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }
    }
}
