using NUnit.Framework;

namespace GymCheckinTests
{
    public class ReviewUtilityTests
    {       

        [Test]
        public void Test_ReviewIsDisplayed()
        {
            NUnit.Framework.Assert.IsFalse(GymCheckin.Utility.ReviewUtility.ShouldRunReview(0));
            NUnit.Framework.Assert.IsTrue(GymCheckin.Utility.ReviewUtility.ShouldRunReview(10));
            NUnit.Framework.Assert.IsFalse(GymCheckin.Utility.ReviewUtility.ShouldRunReview(15));
            NUnit.Framework.Assert.IsFalse(GymCheckin.Utility.ReviewUtility.ShouldRunReview(99));
            NUnit.Framework.Assert.IsTrue(GymCheckin.Utility.ReviewUtility.ShouldRunReview(60));
            NUnit.Framework.Assert.IsTrue(GymCheckin.Utility.ReviewUtility.ShouldRunReview(110));
            NUnit.Framework.Assert.IsFalse(GymCheckin.Utility.ReviewUtility.ShouldRunReview(150));
        }

    }
}