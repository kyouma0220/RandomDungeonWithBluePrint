using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RandomDungeonWithBluePrint
{
    public class RandomMapTest : MonoBehaviour
    {
        [Serializable]
        public class BluePrintWithWeight
        {
            public FieldBluePrint BluePrint = default;
            public int Weight = default;
        }

        [SerializeField] private int seed = default;
        [SerializeField] private Button generateButton = default;
        [SerializeField] private FieldView fieldView = default;
        [SerializeField] private BluePrintWithWeight[] bluePrints = default;

        private void Awake()
        {
            Random.InitState(seed);
            generateButton.onClick.AddListener(() => Create(Raffle()));
            generateButton.onClick.Invoke();
        }

        private void Create(BluePrintWithWeight bluePrint)
        {
            var field = FieldBuilder.Build(bluePrint.BluePrint);
            fieldView.ShowField(field);
        }

        private BluePrintWithWeight Raffle()
        {
            var candidate = bluePrints.ToList();
            var rand = Random.Range(0, candidate.Sum(c => c.Weight));
            var pick = 0;
            for (var i = 0; i < candidate.Count; i++)
            {
                if (rand < candidate[i].Weight)
                {
                    pick = i;
                    break;
                }

                rand -= candidate[i].Weight;
            }

            return candidate[pick];
        }
    }
}
