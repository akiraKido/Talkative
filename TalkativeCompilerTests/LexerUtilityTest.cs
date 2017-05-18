using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalkativeCompiler;

namespace TalkativeCompilerTests
{
    [TestClass]
    public class LexerUtilityTest
    {
        [TestMethod]
        public void ReturnOneTest()
        {
            var lexer = new Lexer( "hoge" );
            var po = new PrivateObject( lexer );

            po.SetFieldOrProperty( "currentPosition", 2 );

            var result = (char)po.Invoke( "ReturnOneChar" );

            Assert.AreEqual( result, 'o' );
            Assert.AreEqual( po.GetFieldOrProperty( "currentPosition" ), 1 );
            Assert.AreEqual( po.GetFieldOrProperty( "lastChar" ), 'o' );
        }
    }
}
