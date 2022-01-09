using System;
using System.Collections.Generic;
using System.Text;

namespace GymCheckin.Utility
{
    public static class ReviewUtility
    {

        public static bool ShouldRunReview(int totalUses)
        {
            int start = 10;
            while (true)
            {
                if (totalUses == start)
                    return true;
                else if ( totalUses > start && totalUses >= (start+50))
                {
                    start = start + 50;
                    continue;
                }
                else if (totalUses < (start + 50))
                {
                    return false;
                }                
            }
        }

    }
}
