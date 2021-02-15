using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solution1
{
    class Program
    {
        public class WeekCombination
        {
           public int Week { get; set; }
           public int order { get; set; }
           public string CombinationWeek { get; set; }
           public List<Combination> Combination { get; set; }
        }
        
        public class Combination
        {
            public string Match { get; set; }
            public string ReversedMatch { get; set; }
            public int First { get; set; }
            public int Last { get; set; }
            public override int GetHashCode()
            {
                return
                    Match.GetHashCode() ^
                    (ReversedMatch ?? String.Empty).GetHashCode();
            }
            public override bool Equals(object obj)
            {
                Combination other = obj as Combination;
                if (obj == null)
                    return false;

                return Match.Equals(other.Match) ||
                        ReversedMatch.Equals(other.Match);
            }
        }

        public static bool CompareLists(List<Combination> list1, List<Combination> list2)
        {
            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;
            Dictionary<Combination, int> hash = new Dictionary<Combination, int>();
            foreach (Combination comb in list1)
            {
                if (hash.ContainsKey(comb))
                {
                    hash[comb]++;
                }
                else
                {
                    hash.Add(comb, 1);
                }
            }

           foreach (Combination comb in list2)
            {
                if (!hash.ContainsKey(comb) || hash[comb] == 0)
                {
                    return false;
                }
                hash[comb]--;
            }

            return true;
        }
        public static List<Combination> GetCombinedWeek(int []R1, int []R2)
        {
            List<Combination> list = new List<Combination>();
            for(int i = 0; i<R1.Length; i++)
            {
                list.Add(new Combination
                {
                    Match = R1[i] + ":" + R2[i],
                    ReversedMatch = R2[i] + ":" + R1[i],
                    First = R1[i],
                    Last = R2[i]
                }); 
            }
            return list;
        }
       public static int[] ShiftArray(int[] array, int shift)
        {
            int[] temp = new int[array.Length];

            for (int index = 0; index < array.Length; index++)
            {
                if (index < shift)
                {
                    temp[index] = array[array.Length - 1 - index];
                }
                else {
                    temp[index] = array[index-shift];
                }
            }

            return temp;
        }
        public static void PrintList(List<string> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine("Java {0}: {1}", (list.IndexOf(item)+1), item);
            }
        }
      //  public static bool CheckExist(int [] array, )

        static void Main(string[] args)
        {
             
            string[] inputStr = new string[5]{
            "1:16,2:15,3:14,4:13,5:12,6:11,7:10,8:9", //w1
            "1:13,14:12,15:11,16:10,2:9,3:8,4:7,5:6",
            "1:4,5:3,6:2,7:16,8:15,9:14,10:13,11:12",
            "1:2,3:16,4:15,5:14,6:13,7:12,8:11,9:10",
            "1:15,16:14,2:13,3:12,4:11,5:10,6:9,7:8"
           };
          
            var arr = inputStr[0].Split(",");
            int arrLength = arr.Length*2;

            if(arrLength % 2 != 0)
            {
                Console.WriteLine("Numri i kombinimeve duhet te jete qiftë");
                return;
            }

            int[] R1 = new int[arrLength/2];
            int[] R2 = new int[arrLength/2];
            int[] Rows = new int[arrLength];
            //int[] RoundArray = new int[arrLength-1];
            int[] StructureArray = new int[arrLength-1];
            
            for (int i = 0; i < arrLength/2; i++)
            {
                var element = arr[i].Split(":");
                Rows[i] = Int32.Parse(element.FirstOrDefault());
                Rows[arrLength-1-i] = Int32.Parse(element.LastOrDefault());
            }
            for(int i = 1; i<arrLength; i++)
            {
                StructureArray[i - 1] = Rows[i];
            }
            List<WeekCombination> weeks = new List<WeekCombination>();
            R1[0] = Rows[0];
            int rowsLength = arrLength / 2;
            for (int w = 0; w <= arrLength-2; w++) {
                StructureArray = ShiftArray(StructureArray, w>0 ? 1:0);
                //RoundArray = StructureArray;
                int[] ArrangedArray = new int[arrLength];
                ArrangedArray[0] = Rows[0];
                for (int i = 1; i < arrLength; i++)
                {
                    ArrangedArray[i] = StructureArray[i - 1];
                }
                for (int i = 0; i< rowsLength; i++)
                { 
                        R1[i] = ArrangedArray[i];
                        R2[i] = ArrangedArray[rowsLength*2 - 1- i];
                }
                weeks.Add(new WeekCombination
                {
                    Combination = GetCombinedWeek(R1, R2),
                    Week = w + 1
                });
            }
            List<WeekCombination> finalList =  new List<WeekCombination>(weeks);
            List<string> showList = new List<string>();
            bool isMatch = true;
            foreach(var item in inputStr)
            {
                if (!isMatch)
                    continue;
                var arrayData = item.Split(",");
                List<Combination> list = new List<Combination>();
                for(int i = 0; i< arrayData.Length; i++)
                {
                    var element = arrayData[i].Split(":");
                    var match = element.FirstOrDefault() + ":" + element.LastOrDefault();
                    var reversedMatch = element.LastOrDefault() + ":" + element.FirstOrDefault();
                    list.Add(new Combination
                    {
                        Match = match,
                        ReversedMatch = reversedMatch
                    });
                }
                bool weekPassed = false;
                foreach(var week in weeks)
                {
                    if (weekPassed)
                        continue;
                    weekPassed = CompareLists(week.Combination, list);

                    if (weekPassed)
                    {
                        finalList.Remove(week);
                        showList.Add(item);
                    }
                }
                isMatch = weekPassed;
            }

            if (isMatch)
            {
                foreach(var item in finalList)
                {
                    showList.Add(String.Join(",", item.Combination.Select(x => x.Match)));
                }
                PrintList(showList);
            }
            else
            {
                Console.WriteLine("Listat me vlera nuk perputhen, kombinimet e futura nuk lejojne qe lista te formoj te gjithe kombinimet e mundshme");
            }


        }
    }
}
