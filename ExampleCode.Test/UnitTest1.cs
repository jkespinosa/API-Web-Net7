using ExampleCode.Test.Services;

namespace ExampleCode.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //API URL

        [Test]
        public void Test_GetUsers()
        {
            UserService userService = new UserService();
            var resultList = userService.GetUsers().Result;
          
            if(resultList.data.Count >0)
            {
                Assert.IsTrue(resultList.data.Count>0);
            }
        }
    }
}