using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ScoreTests
    {        
        [UnityTest]
        public IEnumerator ScoreTestsWithEnumeratorPasses()
        {
            GameObject go = new GameObject();
            ScoreManager sc = go.AddComponent<ScoreManager>();
            sc.scoreText = go.AddComponent<Text>();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(0, sc.score);
            Assert.AreEqual("0", sc.scoreText.text);

            sc.AddScore();
            yield return new WaitForSeconds(1);

            Assert.AreEqual(1, sc.score);
            Assert.AreEqual("1", sc.scoreText.text);

            for (int i = 0; i < 9998; i++) sc.AddScore();
            yield return new WaitForSeconds(1);

            Assert.AreEqual(9999, sc.score);
            Assert.AreEqual("9999", sc.scoreText.text);

            sc.ResetScore();
            yield return new WaitForSeconds(1);

            Assert.AreEqual(0, sc.score);
            Assert.AreEqual("0", sc.scoreText.text);
            yield return null;
        }
    }
}
