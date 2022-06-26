using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ReLearn.Challenges
{
    /*
     ﻿
Blocks = [
"gym": false, "school": true, "store": false,
"gym": true, "school": false, "store": false,
"gym": true, "school": true, "store": false,
"gym": false, "school": true, "store": false,
"gym": false, "school": true, "store": true,
     */
    public class Ameneties
    {
        public static Dictionary<int, bool[]> PrepareIntputs()
        {
            Dictionary<int, bool[]> blocks = new Dictionary<int, bool[]>() {
                { 0,new bool[]{ false,true,false } },
                { 1,new bool[]{ true,false,false } },
                { 2,new bool[]{ true,true,false } },
                { 3,new bool[]{ false,true,false } },
                { 4,new bool[]{ false,true,true } },

            };
            return blocks;
        }
        public static int FindPlotWithNearestAmenities_BruteForce(Dictionary<int, bool[]> blocks)
        {
            Dictionary<int,List<int>> amemetiesDistances = new Dictionary<int, List<int>>();
            int result = -1;
            int minValue = 4;
            for(int i = 0; i < blocks.Count; i++)
            {
                amemetiesDistances.Add(i,new List<int>() { 4,4,4});
                for(int j = 0; j < blocks.Count; j++)
                {
                    for (int amenity = 0; amenity < 3; amenity++)
                    {
                        if (i == j)
                        {
                            if (blocks[i][amenity])
                            {
                                amemetiesDistances[i][amenity] = 0;
                            }
                        }
                        else
                        {
                            if (!blocks[i][amenity] && blocks[j][amenity])
                            {
                                amemetiesDistances[i][amenity] = Math.Min(amemetiesDistances[i][amenity],Math.Abs(i-j));
                            }
                        }
                    }
                }
                if (minValue > amemetiesDistances[i].Max())
                {
                    minValue = amemetiesDistances[i].Max();
                    result = i;
                }
            }
            return result;
        }
    }
}
