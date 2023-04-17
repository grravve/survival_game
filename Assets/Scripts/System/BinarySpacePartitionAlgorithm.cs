using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class BinarySpacePartitionAlgorithm
    {
        public static List<BoundsInt> Generate(BoundsInt splittingArea, int minWidth, int minHeight)
        {
            Queue<BoundsInt> areasQueue = new Queue<BoundsInt>();
            List<BoundsInt> areasList = new List<BoundsInt>();

            areasQueue.Enqueue(splittingArea);

            while (areasQueue.Count > 0)
            {
                var area = areasQueue.Dequeue();

                if (area.size.x < minWidth && area.size.y < minHeight)
                {
                    continue;
                }

                bool horizontallSplitCheckFirst = Random.value < 0.5f;

                if (horizontallSplitCheckFirst)
                {
                    if (area.size.x >= minWidth * 2)
                    {
                        SplitHorizontally(areasQueue, area);

                        continue;
                    }

                    if (area.size.y >= minHeight * 2)
                    {
                        SplitVertically(areasQueue, area);

                        continue;
                    }

                    areasList.Add(area);
                }

                if (!horizontallSplitCheckFirst)
                {
                    if (area.size.y >= minHeight * 2)
                    {
                        SplitVertically(areasQueue, area);

                        continue;
                    }

                    if (area.size.x >= minWidth * 2)
                    {
                        SplitHorizontally(areasQueue, area);

                        continue;
                    }

                    areasList.Add(area);
                }
            }

            return areasList;
        }

        private static void SplitVertically(Queue<BoundsInt> areasQueue, BoundsInt splittingArea)
        {
            int xSplit = Random.Range(1, splittingArea.x);

            Vector3Int leftAreaSize = new Vector3Int(xSplit, splittingArea.size.y, splittingArea.size.z);
            Vector3Int rightAreaSize = new Vector3Int(splittingArea.size.x - xSplit, splittingArea.size.y, splittingArea.size.z);

            BoundsInt leftArea = new BoundsInt(splittingArea.min, leftAreaSize);
            BoundsInt rightArea = new BoundsInt(new Vector3Int(splittingArea.min.x + xSplit, splittingArea.min.y, splittingArea.min.z), rightAreaSize);

            areasQueue.Enqueue(leftArea);
            areasQueue.Enqueue(rightArea);
        }

        private static void SplitHorizontally(Queue<BoundsInt> areasQueue, BoundsInt splittingArea)
        {
            int ySplit = Random.Range(1, splittingArea.y);

            Vector3Int downAreaSize = new Vector3Int(splittingArea.size.x, ySplit, splittingArea.size.z);
            Vector3Int upAreaSize = new Vector3Int(splittingArea.size.x, splittingArea.size.y - ySplit, splittingArea.size.z);

            BoundsInt downArea = new BoundsInt(splittingArea.min, downAreaSize);
            BoundsInt upArea = new BoundsInt(new Vector3Int(splittingArea.min.x, splittingArea.min.y + ySplit, splittingArea.min.z), upAreaSize);

            areasQueue.Enqueue(downArea);
            areasQueue.Enqueue(upArea);
        }
    }
}

