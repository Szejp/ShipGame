using UnityEngine;

namespace QFramework.Modules.QConfig.Scripts
{
    [CreateAssetMenu(fileName = "TestConfig", menuName = "Q/TestConfig")]
    public class TestConfig : ConfigBase
    {
        [SerializeField] string testField;
    }
}