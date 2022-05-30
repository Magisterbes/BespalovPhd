using System;
using System.Linq;
//Dr. James McCaffrey of Microsoft Research code

namespace MedicalModel
{
    class LogisticMultiRegression
    {
        static public double[][] GetModelByTrainData(double[][] trainX, int[][] trainY)
        {

            double lr = 0.01;
            int maxEpoch = 1000;
            Environment.LogInfo.Add("\nStart SGD train: lr = " +
            lr.ToString("F3") + ", maxEpoch = " + maxEpoch);
            double[][] wts = Train(trainX, trainY, lr, maxEpoch);
            Environment.LogInfo.Add("Done");

            //Console.WriteLine("\nModel weights and biases:");
            //ShowMatrix(wts);

            //Console.WriteLine("\nPredicting class");
            //double[] x = new double[] { 50, 30, 1 };
            //double[] oupts = ComputeOutput(x, wts, true);  // true: show pre-softmax
            //ShowVector(oupts);

            // Console.WriteLine("\nEnd demo");
            // Console.ReadLine();

            return wts;
        }

        static public double[] ComputeOutput(double[] x, double[][] wts, bool verbose = false)
        {
            // wts[feature][class]
            // biases are in last row of wts
            int nc = wts[0].Length;  // number classes
            int nf = x.Length;  // number features
            double[] outputs = new double[nc];
            for (int j = 0; j < nc; ++j)
            {
                for (int i = 0; i < nf; ++i)
                    outputs[j] += x[i] * wts[i][j];
                outputs[j] += wts[nf][j];  // add bias 
            }

            //if (verbose == true)
            //  ShowVector(outputs);  // pre-softmax

            return Softmax(outputs);
        }

        static double[] Softmax(double[] vec)
        {
            // naive. consider max trick
            double[] result = new double[vec.Length];
            double sum = 0.0;
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = Math.Exp(vec[i]);
                sum += result[i];
            }
            for (int i = 0; i < result.Length; ++i)
                result[i] /= sum;
            return result;
        }

        static double[][] Train(double[][] trainX, int[][] trainY, double lr, int maxEpoch, int seed = 0)
        {
            double best_err = 100;
            int N = trainX.Length;  // number train items
            int nf = trainX[0].Length;  // number predictors/features
                                        //int nw = nf + 1;  // plus 1 for the bias
            int nc = trainY[0].Length;  // number classes
            Random rnd = new Random(seed);


            double[][] wts = new double[nf + 1][];  // 1 extra row for biases
            for (int i = 0; i < wts.Length; ++i)
                wts[i] = new double[nc];  // wts[i][j] - j is the class, i is the wt (b last cell)

            double[][] best_wts = new double[nf + 1][];

            double lo = -0.01; double hi = 0.01;
            for (int i = 0; i < wts.Length; ++i)
                for (int j = 0; j < wts[0].Length; ++j)
                    wts[i][j] = (hi - lo) * rnd.NextDouble() + lo;

            int[] indices = new int[N];  // process in random order
            for (int i = 0; i < N; ++i)
                indices[i] = i;

            for (int epoch = 0; epoch < maxEpoch; ++epoch)
            {
                Shuffle(indices, rnd);
                foreach (int idx in indices)  // each train item
                {
                    double[] oupts = ComputeOutput(trainX[idx], wts);  // computed like (0.20, 0.50, 0.30)
                    int[] targets = trainY[idx];                       // targets like  ( 0,     1,    0)
                    for (int j = 0; j < nc; ++j)
                    {  // each class
                        for (int i = 0; i < nf; ++i)
                        {  // each feature
                            wts[i][j] += -1 * lr * trainX[idx][i] * (oupts[j] - targets[j]) * oupts[j] * (1 - oupts[j]);
                        }
                        wts[nf][j] += -1 * lr * 1 * (oupts[j] - targets[j]) * oupts[j] * (1 - oupts[j]);
                    } // j
                } // each train item

                var local_err = Error(trainX, trainY, wts);
               // Console.WriteLine(local_err);
                if (local_err< best_err)
                {
                    best_wts = wts.Select(a => a.ToArray()).ToArray();
                    best_err = local_err;
                }

            } // epoch

            double err = Error(trainX, trainY, best_wts);
            double acc = Accuracy(trainX, trainY, best_wts);
            Environment.LogInfo.Add("epoch = " + maxEpoch.ToString()
                        + ", acc = " + acc.ToString("F4")
                        + ", err = " + err.ToString("F4"));

            return best_wts;
        } // Train

        static void Shuffle(int[] vec, Random rnd)
        {
            int n = vec.Length;
            for (int i = 0; i < n; ++i)
            {
                int ri = rnd.Next(i, n);
                int tmp = vec[ri];
                vec[ri] = vec[i];
                vec[i] = tmp;
            }
        }

        static double Accuracy(double[][] dataX, int[][] dataY, double[][] wts)
        {
            int N = dataX.Length;
            int numCorrect = 0; int numWrong = 0;

            for (int i = 0; i < N; ++i)
            {
                double[] oupts = ComputeOutput(dataX[i], wts);
                int[] targets = dataY[i];

                int mi = ArgMax(oupts);
                if (targets[mi] == 1)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (numCorrect * 1.0) / (numCorrect + numWrong);
        }

        static int ArgMax(double[] vec)
        {
            double maxVal = vec[0];
            int maxIdx = 0;
            for (int i = 0; i < vec.Length; ++i)
            {
                if (vec[i] > maxVal)
                {
                    maxVal = vec[i]; maxIdx = i;
                }
            }
            return maxIdx;
        }

        static double Error(double[][] dataX, int[][] dataY, double[][] wts)
        {
            // mean squared error
            int N = dataX.Length;
            int nc = dataY[0].Length;  // number classes

            double sumSqErr = 0.0;
            for (int i = 0; i < N; ++i)
            {
                double[] oupts = ComputeOutput(dataX[i], wts);
                int[] targets = dataY[i];

                for (int j = 0; j < nc; ++j)
                    sumSqErr += (oupts[j] - targets[j]) * (oupts[j] - targets[j]);
            }
            return sumSqErr / N;
        }

        static void ShowVector(double[] vec)
        {
            for (int i = 0; i < vec.Length; ++i)
                Console.Write(vec[i].ToString("F4") + "  ");
            Console.WriteLine("");
        }

        static void ShowVector(int[] vec)
        {
            for (int i = 0; i < vec.Length; ++i)
                Console.Write(vec[i] + "  ");
            Console.WriteLine("");
        }

        static void ShowMatrix(double[][] wts)
        {
            Console.WriteLine("        Stage 1   Stage 2  Stage 3 Stage 4");
            for (int i = 0; i < wts.Length; ++i)
            {
                if (i == wts.Length - 1)
                    Console.Write("b   ");
                else
                    Console.Write("w[" + i + "]");
                for (int j = 0; j < wts[0].Length; ++j)
                {
                    Console.Write(wts[i][j].ToString("F4").PadLeft(10));
                }
                Console.WriteLine("");
            }
        }

    } // Program class

} // ns