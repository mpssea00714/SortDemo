using System;

namespace SortDemo
{
    class Program
    {
        public static void Main()
        {
            int[] datas = { 41, 33, 17, 80, 61, 5, 55 };
            var t = new Test();
            Console.WriteLine($"-----SelectSort-----");
            t.SelectSort(datas);
            Console.WriteLine($"-----BubbleSort-----");
            t.BubbleSort(datas);
            Console.WriteLine($"-----InsertSort-----");
            t.InsertSort(datas);
            Console.WriteLine($"-----MergeSort-----");
            t.MergeSort(datas);
            Console.WriteLine($"-----QuickSort-----");
            t.QuickSort(datas);
            Console.ReadLine();
        }
    }

    public class Test
    {
        public void SelectSort(int[] nums)
        {
            var tmp = 0;
            var AryLength = nums.Length;
            // ***從index=0的第一個值開始，每次遞增就跟下層迴圈比出來的最小值的index位置交換
            for (var i = 0; i < AryLength - 1; i++)
            {
                var minIndex = i;
                //Console.WriteLine($"Round-{i + 1}");
                //Console.WriteLine($"i-{i}");

                // ***跟其他index的值比較，run過一遍取得未排序元素中(i 的右邊)最小值的Index
                for (var j = i + 1; j < AryLength; j++)
                {
                    if (nums[minIndex] > nums[j])
                    {
                        minIndex = j;
                        //Console.WriteLine($"minIndex-{minIndex}");
                    }
                }
                // ***將最小值移到i值的位置，i 的左邊就是已排序過的元素
                tmp = nums[i];
                nums[i] = nums[minIndex];
                nums[minIndex] = tmp;
            }
            Console.WriteLine($"result-{string.Join(",", nums)}");
        }

        public void BubbleSort(int[] nums)
        {
            var AryLength = nums.Length;
            //***隨著i的遞減，每輪i的值，都是下層迴圈比較出來該層的最大值
            for (var i = AryLength - 1; i > 0; i--)
            {
                //Console.WriteLine($"Round-{i}");
                var tmp = 0;
                //***跟其他index的值比較，不斷透過位置交換把最大值往右邊移
                for (var j = 0; j < i; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        tmp = nums[j + 1];
                        nums[j + 1] = nums[j];
                        nums[j] = tmp;
                    }
                }
            }
            Console.WriteLine($"result-{string.Join(",", nums)}");
        }

        public void InsertSort(int[] nums)
        {
            var AryLength = nums.Length;
            //***隨著i的遞增，把每輪的i值插入到第一個比i小的前一個位置
            for (var i = 1; i < AryLength; i++)
            {
                //Console.WriteLine($"Round-{i}");
                var tmp = nums[i];
                //Console.WriteLine($"tmp-{tmp}");
                var j = i - 1;
                //***從nums[i]的值開始，向左邊的數字比較，把較大的數字往右邊推
                while (j >= 0 && nums[j] > tmp)
                {
                    Console.WriteLine($"j-{j}");
                    nums[j + 1] = nums[j];
                    j--;
                }
                //***把i值插入相對的位置
                nums[j + 1] = tmp;
            }
            Console.WriteLine($"result-{string.Join(",", nums)}");
        }

        //***使用分治法(Divide and conquer)，將該陣列拆成一半一半的。
        public void MergeSort(int[] ary)
        {
            SortAry(ary, 0, ary.Length - 1);
            Console.WriteLine($"result-{String.Join(" ", ary)}");
        }

        void SortAry(int[] arr, int l, int r)
        {
            if (l < r)
            {
                //Console.WriteLine($"L-length-{l}   R-length-{r}");
                var m = (l + r) / 2;
                SortAry(arr, l, m);
                SortAry(arr, m + 1, r);
                //Console.WriteLine($"L-length-{l}  M-{m}   R-length-{r}");

                //***拆到最後會變成各剩一個值，再將值一個一個排序組合回去。
                Merge(arr, l, m, r);

            }
        }

        void Merge(int[] arr, int l, int m, int r)
        {
            var n1 = m - l + 1;
            var n2 = r - m;
            int[] L = new int[n1];
            int[] R = new int[n2];

            for (var a = 0; a < n1; a++)
            {
                L[a] = arr[l + a];
            }
            for (var b = 0; b < n2; b++)
            {
                R[b] = arr[m + 1 + b];
            }
            //Console.WriteLine($"L[]-{String.Join(" ", L)}");
            //Console.WriteLine($"R[]-{String.Join(" ", R)}");
            //Console.WriteLine($"Before Sort-{String.Join(" ", arr)}");
            var i = 0;
            var j = 0;
            var k = l;

            //***將每個階段的小陣列，做左右陣列的比較排序
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            //***上面的陣列比較排序完成，代表左右其中一個陣列已經全部插入陣列中，剩下就把還沒排入陣列中的值依序排在後面就好
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
            //Console.WriteLine($"After Sort-{String.Join(" ", arr)}");
            //Console.WriteLine($"--------------------------------------");
        }

        //***使用分治法(Divide and conquer)，設定一個值為基準點(key值)，
        public void QuickSort(int[] arr)
        {
            RunQuickSort(arr, 0, arr.Length - 1);
            Console.WriteLine($"result-{String.Join(" ", arr)}");
        }

        void RunQuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            var key = arr[left];
            var i = left;
            var j = right;

            //***以key值各從左右兩邊與key值比較，把(從左邊向右比key值大的值)與(從右邊向左比key小的值)交換，不斷比較交換，直到i==j
            while (i != j)
            {
                while (arr[j] > key && i < j)
                {
                    j--;
                }

                while (arr[i] <= key && i < j)
                {
                    i++;
                }

                if (i < j)
                {
                    var temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            //***再把key值與i==j的值交換，再將i==j的值的位置做分斷點
            arr[left] = arr[i];
            arr[i] = key;

            //***拆掉的左右兩個陣列，再重複做一次以上的步驟
            RunQuickSort(arr, left, j - 1);
            RunQuickSort(arr, j + 1, right);
        }
    }
}
