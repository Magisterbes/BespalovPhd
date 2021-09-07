using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MedicalModel
{

    class Parameters
    {
        private int yearsToSimulate = 60;
        private int initPopulation = 1000000;
        private double maleVsFemale = 0.485;
        private int unrealLifeLength = 110;
        private double meanBornProbabilityPerYear = 0.034;
        private double lowerestFertilityAge = 15;
        private double highestFertilityAge = 44;


        private Distiribution maleAging = new Distiribution(new double[] { 839, 86, 58, 47, 43, 39, 33, 35, 41, 32, 31, 39, 35, 43, 49, 67, 97, 118, 148, 168, 202, 241, 257, 283, 295, 338, 381, 434, 490, 540, 626, 641, 688, 692, 752, 783, 782, 778, 797, 814, 906, 900, 949, 1047, 1073, 1197, 1233, 1292, 1398, 1462, 1641, 1726, 1848, 1930, 2138, 2395, 2485, 2572, 2848, 2916, 3424, 3875, 3511, 4317, 3884, 4343, 4671, 4321, 5618, 5442, 5998, 6777, 6589, 7304, 7846, 8431, 8658, 9403, 10182, 10599, 11047, 11591, 12912, 13513, 14484, 15334, 17155, 17034, 19416, 20608, 22171, 25642, 25576, 27301, 28808, 29668, 31233, 32807, 34384, 35957, 37517, 39059, 40577, 42063, 43513, 44922, 46285, 47599, 48861, 50069, 100000 },DistributionInputType.PDF);
        private Distiribution femaleAging = new Distiribution(new double[] { 670, 77, 43, 35, 30, 30, 22, 23, 22, 20, 23, 22, 24, 28, 33, 43, 47, 52, 59, 70, 71, 73, 79, 81, 88, 96, 114, 129, 139, 158, 177, 186, 198, 199, 207, 233, 230, 233, 250, 265, 286, 302, 316, 329, 344, 364, 376, 412, 435, 470, 515, 563, 589, 617, 688, 798, 852, 908, 998, 1074, 1165, 1313, 1223, 1566, 1465, 1670, 1824, 1760, 2253, 2215, 2564, 2983, 3090, 3634, 4050, 4560, 4828, 5540, 6336, 6946, 7783, 8562, 9757, 10581, 11620, 12888, 14532, 15020, 17174, 18272, 20413, 23018, 23792, 24922, 27601, 29388, 31353, 33332, 35311, 37276, 39214, 41111, 42957, 44740, 46453, 48087, 49638, 51101, 52474, 53757, 100000 }, DistributionInputType.PDF);

        private Distiribution initAgeDistMale = new Distiribution(new double[] { 0.00746614481409002, 0.00746301369863014, 0.00748845401174168, 0.00742133072407045, 0.00746555772994129, 0.00749980430528376, 0.00744970645792564, 0.00747397260273973, 0.00746262230919765, 0.0074559686888454, 0.00751624266144814, 0.00747866927592955, 0.00750489236790607, 0.00742700587084149, 0.00742504892367906, 0.00740078277886497, 0.00740450097847358, 0.00742426614481409, 0.00739491193737769, 0.00743365949119374, 0.00738238747553816, 0.0073706457925636, 0.00736888454011742, 0.00732954990215264, 0.00731506849315069, 0.00728767123287671, 0.00723737769080235, 0.00722485322896282, 0.00717123287671233, 0.00712700587084149, 0.00717534246575342, 0.00707240704500979, 0.00701565557729941, 0.00693307240704501, 0.00695068493150685, 0.00690841487279843, 0.00680313111545988, 0.0067866927592955, 0.00666908023483366, 0.00665420743639922, 0.00660508806262231, 0.00646908023483366, 0.00646183953033268, 0.00638336594911937, 0.00635968688845401, 0.00619745596868885, 0.00618219178082192, 0.0060839530332681, 0.00602035225048924, 0.00594911937377691, 0.00581311154598826, 0.00574461839530333, 0.00567005870841487, 0.00557025440313112, 0.00534461839530333, 0.00525225048923679, 0.00508493150684932, 0.00499608610567515, 0.00484833659491194, 0.00470078277886497, 0.00455753424657534, 0.00442954990215264, 0.00421545988258317, 0.00403424657534247, 0.00388101761252446, 0.00376379647749511, 0.00356457925636008, 0.00338023483365949, 0.00321409001956947, 0.00300450097847358, 0.00285557729941292, 0.00262211350293542, 0.00248375733855186, 0.00232876712328767, 0.00209158512720157, 0.00194442270058708, 0.00177436399217221, 0.00161095890410959, 0.00141291585127202, 0.00128904109589041, 0.0011399217221135, 0.00100763209393346, 0.000873776908023483, 0.00076692759295499, 0.000632681017612524, 0.000550684931506849, 0.000452054794520548, 0.000385714285714286, 0.000309197651663405, 0.000233659491193738, 0.000180039138943249, 0.000139334637964775, 0.000102348336594912, 0.0000743639921722114, 0.0000555772994129159, 0.0000373776908023483, 0.00002426614481409, 0.0000181996086105675, 0.0000107632093933464, 6.26223091976517E-06, 4.50097847358121E-06, 2.34833659491194E-06, 1.95694716242661E-06, 9.78473581213307E-07, 5.87084148727984E-07, 0, 0, 0, 0, 0 }, DistributionInputType.PDF);
        private Distiribution initAgeDistFemale = new Distiribution(new double[] { 0.0070986301369863, 0.00709295499021526, 0.00704148727984344, 0.00710234833659491, 0.00707690802348337, 0.00704657534246575, 0.00707866927592955, 0.00703972602739726, 0.00703189823874755, 0.00704500978473581, 0.00698904109589041, 0.00701154598825832, 0.00699041095890411, 0.00707318982387476, 0.00707534246575342, 0.00707769080234834, 0.00705283757338552, 0.00704148727984344, 0.00705048923679061, 0.00698962818003914, 0.00702191780821918, 0.00700782778864971, 0.00697514677103718, 0.0070091976516634, 0.00699921722113503, 0.00698904109589041, 0.00696712328767123, 0.00696731898238748, 0.00696712328767123, 0.00697005870841487, 0.00688101761252446, 0.00691428571428571, 0.00691252446183953, 0.00690176125244618, 0.00684207436399217, 0.00682093933463796, 0.00684187866927593, 0.00680039138943249, 0.00683522504892368, 0.00679001956947162, 0.00674050880626223, 0.00681839530332681, 0.00672426614481409, 0.006726614481409, 0.0066866927592955, 0.00668101761252446, 0.00665029354207436, 0.00664579256360078, 0.00657436399217221, 0.00655440313111546, 0.00649686888454012, 0.00646888454011742, 0.00641643835616438, 0.00634011741682975, 0.00638434442270059, 0.00632348336594912, 0.00629432485322896, 0.00619843444227006, 0.00613639921722114, 0.00606947162426614, 0.00599197651663405, 0.00588982387475538, 0.00585675146771037, 0.00579315068493151, 0.00564853228962818, 0.00554794520547945, 0.00547984344422701, 0.0054027397260274, 0.00523953033268102, 0.00521095890410959, 0.00503620352250489, 0.00487651663405088, 0.00470254403131115, 0.00448590998043053, 0.00432681017612524, 0.00412896281800391, 0.00396203522504892, 0.00370469667318982, 0.0034972602739726, 0.00324716242661448, 0.00300117416829746, 0.00274422700587084, 0.00250841487279843, 0.00218199608610567, 0.0019853228962818, 0.00171545988258317, 0.00142994129158513, 0.00123972602739726, 0.00103052837573386, 0.000808610567514677, 0.000656751467710372, 0.000510958904109589, 0.000391780821917808, 0.000290606653620352, 0.000207436399217221, 0.000157925636007828, 0.000105283757338552, 0.0000698630136986301, 0.0000450097847358121, 0.000026027397260274, 0.0000148727984344423, 0.0000105675146771037, 5.28375733855186E-06, 3.91389432485323E-06, 1.56555772994129E-06, 9.78473581213307E-07, 1.95694716242661E-07, 1.95694716242661E-07, 0, 0 }, DistributionInputType.PDF);
        private Distiribution migrationDistr = new Distiribution(new double[] { 1.52414717587954e-05, 4.78045734205812e-05, 0.000102416671915596, 0.000190488111861647, 0.000326926521887085, 0.000530139629854598, 0.000820882808012271, 0.00122048993591589, 0.00174841965372469, 0.00241868768867301, 0.00323637147671981, 0.00419484623462548, 0.00527432602061642, 0.00644233730789695, 0.00765635820723645, 0.00886835666122765, 0.0100298924218496, 0.0110981374965563, 0.0120399328803755, 0.0128345637748185, 0.0134743929951909, 0.0139632421672649, 0.0143138501405269, 0.0145441698657856, 0.0146743785219500, 0.0147239153183948, 0.0147099678130719, 0.0146461406421882, 0.0145427823656420, 0.0144071706676200, 0.0142439088716543, 0.0140555662481710, 0.0138433154995790, 0.0136073538262254, 0.0133469877016459, 0.0130610673208536, 0.0127481342900343, 0.0124065365338702, 0.0120346998252970, 0.0116312075472458, 0.0111951969635124, 0.0107263735977541, 0.0102252362918401, 0.00969324165356875, 0.00913282796422387, 0.00854743056376824, 0.00794155631352293, 0.00732064240665324, 0.00669095696852800, 0.00605945329068050, 0.00543355380895593, 0.00482090639456396, 0.00422909048541161, 0.00366530503163543, 0.00313604901881413, 0.00264682035885623, 0.00220186371077012, 0.00180398845792569, 0.00145447609214305, 0.00115308671880947, 0.000898164174725018, 0.000686828848412361, 0.000515238163152737, 0.000378888122367188, 0.000272926272921342, 0.000192447332169788, 0.000132747316252328, 8.95194193331352e-05, 5.89837570617090e-05, 3.79518411563580e-05, 2.38338933065369e-05, 1.46018910907061e-05, 8.72323630865122e-06, 5.07941058199229e-06, 2.88162204069740e-06, 1.59211876405782e-06, 8.56365109239511e-07, 4.48248929947696e-07, 2.28237791125780e-07, 1.13003758623919e-07, 5.43827161451434e-08, 2.54280458880218e-08, 1.15469158451910e-08, 5.09016996053975e-09, 2.17730960143831e-09, 9.03306040544519e-10, 3.63311906260212e-10, 1.41597886234731e-10, 5.34526740804991e-11, 1.95354229269575e-11, 6.90916465574888e-12, 2.36369208178943e-12, 7.81875961932788e-13, 2.49972206891664e-13, 7.72119068387044e-14, 2.30330919661716e-14, 6.63335158145468e-15, 1.84354197101126e-15, 4.94186253455448e-16, 1.27675285609101e-16, 3.17501109401103e-17, 7.57860907759609e-18, 1.72595230642975e-18, 3.71162605563335e-19, 7.36440490470629e-20, 1.27988170644743e-20, 1.49133640283259e-21, 0, 0, 0, 1.47149131378942e-05, 4.63331071290425e-05, 9.93381545201280e-05, 0.000184859268655655, 0.000317338615110279, 0.000514575916572094, 0.000796858789073742, 0.00118501699603090, 0.00169787275949970, 0.00234907733168782, 0.00314366298067516, 0.00407523453627419, 0.00512471623069895, 0.00626067184160938, 0.00744198937388265, 0.00862203000581073, 0.00975416066648065, 0.0107968294432476, 0.0117182481068410, 0.0124986363052041, 0.0131308964172799, 0.0136191241060112, 0.0139761400763758, 0.0142198463201994, 0.0143702395218725, 0.0144466900728994, 0.0144662100734090, 0.0144426412853568, 0.0143862527011465, 0.0143043259609243, 0.0142014980169641, 0.0140805182624888, 0.0139424284930716, 0.0137875448883353, 0.0136151591745773, 0.0134240955069886, 0.0132126839922153, 0.0129791107753245, 0.0127213211643442, 0.0124371468362177, 0.0121244962131360, 0.0117813853365823, 0.0114061017185826, 0.0109972016562695, 0.0105538222918912, 0.0100756174475715, 0.00956304964960148, 0.00901747207801093, 0.00844131285598222, 0.00783823039444820, 0.00721323038879449, 0.00657271057567892, 0.00592438523115560, 0.00527708729848540, 0.00464042411533977, 0.00402430273987344, 0.00343836627428371, 0.00289139405734636, 0.00239073396582161, 0.00194183594677446, 0.00154794531907024, 0.00120999331801958, 0.000926694331112114, 0.000694829688937382, 0.000509672572964076, 0.000365492377743434, 0.000256072280986267, 0.000175180628328089, 0.000116952340298839, 7.61566582153002e-05, 4.83476227435563e-05, 2.99100212982785e-05, 1.80239988067769e-05, 1.05756672049986e-05, 6.03983861430645e-06, 3.35621991930931e-06, 1.81399443738663e-06, 9.53322424168620e-07, 4.86993703950228e-07, 2.41740890263507e-07, 1.16569423494462e-07, 5.45872687252687e-08, 2.48161081126551e-08, 1.09489693879445e-08, 4.68672536247468e-09, 1.94571573408220e-09, 7.83172249368386e-10, 3.05530982462396e-10, 1.15484423117707e-10, 4.22776188335582e-11, 1.49852752386043e-11, 5.14084287934227e-12, 1.70635193981880e-12, 5.47795521624182e-13, 1.70034394120375e-13, 5.10125896817332e-14, 1.47873940748487e-14, 4.14015089628212e-15, 1.11904314235564e-15, 2.91786189976401e-16, 7.33002442579838e-17, 1.76934456510101e-17, 4.08055749580479e-18, 8.90821233636040e-19, 1.80979211228454e-19, 3.29650857470664e-20, 4.69589126526446e-21, 3.27371927562490e-22, 0, 0 }, DistributionInputType.PDF);


        private Distiribution maleIncidenceProbs;
        private Distiribution femaleIncidenceProbs;

        private double[] cancerTransitions = new double[] { 2.5, 2.5, 3.5, 1.5 };
        private double diagnoseTransition = 6.7;

        private double[] treatmentEfficiency = new double[] { 0.75, 0.85, 0.95 };
        private double[] complications = new double[] { 0.1 };
        private double[] ageCureConstants = new double[] { 1.062, 1.0601, 1.058, 1.0557, 1.0532, 1.0505, 1.0476, 1.0445, 1.0412, 1.0377, 1.034, 1.0301, 1.026, 1.0217, 1.0172, 1.0125, 1.0076, 1.0025, 0.9972, 0.9917, 0.9860, 0.9801, 0.9740, 0.9677, 0.9612, 0.9545, 0.9476, 0.9405, 0.9332, 0.9257, 0.9180, 0.9101, 0.9020, 0.8937, 0.8852, 0.8765, 0.8676, 0.8585, 0.8492, 0.8397, 0.8300, 0.8201, 0.8100, 0.7997, 0.7892, 0.7785, 0.7676, 0.7565, 0.7452, 0.7337, 0.7220, 0.7101, 0.6980, 0.6857, 0.6732, 0.6605, 0.6476, 0.6345, 0.6212, 0.6077, 0.5940, 0.5801, 0.5660, 0.5517, 0.5372, 0.5225, 0.5076, 0.4925, 0.4772, 0.4617, 0.4460, 0.4301, 0.4140, 0.3977, 0.3812, 0.3645, 0.3476, 0.3305, 0.3132, 0.2957, 0.2780, 0.2601, 0.2420, 0.2237, 0.2052, 0.1865, 0.1676, 0.1485, 0.1292, 0.1097, 0.09001, 0.07011, 0.05000, 0.0296999999999998, 0.009210, -0.01151, -0.03240, -0.0534999999999999, -0.07480, -0.09631, -0.1180, -0.1399, -0.1620, -0.1843, -0.2068, -0.2295, -0.2524, -0.2755, -0.2988, -0.3223, -0.3460, -0.3699, -0.3940, -0.4183, -0.4428, -0.4675, -0.4924, -0.5175, -0.5428, -0.5683, -0.5940 };

        private List<Factor> factors = new List<Factor>();
        private double[] factorAgeCorrection = { 0.1, 2 };

        private int screeningDate = 30;
        private int startAge = 50;
        private int finishAge = 60;
        private int freqency = 2;
        private int selectedTest = 70;
        private double participationRate = 0.7;


        private double[] testParameters = { 50, 60, 70, 80 };
        private double[] testTP = { 0.6, 0.7, 0.9, 0.95 };
        private double[] testFP = { 0.05, 0.07, 0.1, 0.15 };


        private double testPerPersonPrice = 1;
        private double screeningPrice = 1;
        private double[] stageTreatementPrice = { 1, 2, 3, 4 };
        private double complicationsPrice = 1;

        public int YearsToSimulate { get => yearsToSimulate; set => yearsToSimulate = value; }
        public int InitPopulation { get => initPopulation; set => initPopulation = value; }
        public double MaleVsFemale { get => maleVsFemale; set => maleVsFemale = value; }
        public int UnrealLifeLength { get => unrealLifeLength; set => unrealLifeLength = value; }
        public double MeanBornProbabilityPerYear { get => meanBornProbabilityPerYear; set => meanBornProbabilityPerYear = value; }
        public double LowerestFertilityAge { get => lowerestFertilityAge; set => lowerestFertilityAge = value; }
        public double HighestFertilityAge { get => highestFertilityAge; set => highestFertilityAge = value; }
        public Distiribution MaleAging { get => maleAging; set => maleAging = value; }
        public Distiribution FemaleAging { get => femaleAging; set => femaleAging = value; }
        public Distiribution InitAgeDistMale { get => initAgeDistMale; set => initAgeDistMale = value; }
        public Distiribution InitAgeDistFemale { get => initAgeDistFemale; set => initAgeDistFemale = value; }
        public Distiribution MigrationDistr { get => migrationDistr; set => migrationDistr = value; }
        public Distiribution MaleIncidenceProbs { get => maleIncidenceProbs; set => maleIncidenceProbs = value; }
        public Distiribution FemaleIncidenceProbs { get => femaleIncidenceProbs; set => femaleIncidenceProbs = value; }
        public double[] CancerTransitions { get => cancerTransitions; set => cancerTransitions = value; }
        public double DiagnoseTransition { get => diagnoseTransition; set => diagnoseTransition = value; }
        public double[] TreatmentEfficiency { get => treatmentEfficiency; set => treatmentEfficiency = value; }
        public double[] Complications { get => complications; set => complications = value; }
        public double[] AgeCureConstants { get => ageCureConstants; set => ageCureConstants = value; }
        internal List<Factor> Factors { get => factors; set => factors = value; }
        public double[] FactorAgeCorrection { get => factorAgeCorrection; set => factorAgeCorrection = value; }
        public int ScreeningDate { get => screeningDate; set => screeningDate = value; }
        public int StartAge { get => startAge; set => startAge = value; }
        public int FinishAge { get => finishAge; set => finishAge = value; }
        public int Freqency { get => freqency; set => freqency = value; }
        public int SelectedTest { get => selectedTest; set => selectedTest = value; }
        public double ParticipationRate { get => participationRate; set => participationRate = value; }
        public double[] TestParameters { get => testParameters; set => testParameters = value; }
        public double[] TestTP { get => testTP; set => testTP = value; }
        public double[] TestFP { get => testFP; set => testFP = value; }
        public double TestPerPersonPrice { get => testPerPersonPrice; set => testPerPersonPrice = value; }
        public double ScreeningPrice { get => screeningPrice; set => screeningPrice = value; }
        public double[] StageTreatementPrice { get => stageTreatementPrice; set => stageTreatementPrice = value; }
        public double ComplicationsPrice { get => complicationsPrice; set => complicationsPrice = value; }

        public Parameters()
        {
        }

        public Parameters(string filename)
        {
            var lis = File.ReadAllText(filename).Split('\n').ToList();
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            foreach (var row in lis)
            {
                if (row.Trim() == "")
                    continue;

                if (row[0] == '#')
                    continue;

                var spl = row.Split(':');

                if (spl[0] == "FactorsRR")
                {
                    var fspl = spl[1].Split(',').Select(a =>
                    {
                        var ffspl = a.Split('@');
                        return new Factor(Convert.ToDouble(ffspl[1]), ffspl[0], true);
                    }).ToList<Factor>();
                }
                else if (spl[0] == "AgeIrrelevantFactorsRR")
                {
                    var fspl = spl[1].Split(',').Select(a =>
                    {
                        var ffspl = a.Split('@');
                        return new Factor(Convert.ToDouble(ffspl[1]), ffspl[0], false);
                    }).ToList<Factor>();
                }
                else
                {
                    PropertyInfo propertyInfo = this.GetType().GetProperty(spl[0].Trim());
                    var type = propertyInfo.PropertyType.FullName;

                    if (type == "System.Double[]")
                    {
                        double[] doubles = spl[1].Split(',').Select(Double.Parse).ToArray();
                        propertyInfo.SetValue(this, doubles, null);
                    }
                    else if (type == "MedicalModel.Distiribution")
                    {
                        double[] doubles = spl[1].Split(',').Select(Double.Parse).ToArray();

                        //Three times more often
                        if(spl[0] == "InitAgeDistMale" || spl[0] == "InitAgeDistFemale")
                        {
                            doubles = doubles.Select(a => a *3).ToArray();
                        }

                        if (spl[0] == "MaleAging" || spl[0] == "FemaleAging")
                        {

                            doubles = FromRisksToDistribution(doubles.Select(a => a / 100000).ToArray());
                        }

                        propertyInfo.SetValue(this, new Distiribution(doubles, DistributionInputType.PDF), null);
                    }
                    else
                    {
                        var convdata = Convert.ChangeType(spl[1].Trim(), Type.GetType(type));

                        propertyInfo.SetValue(this, convdata, null);
                    }
                }
            }

            

     

        }

        private double[] FromRisksToDistribution(double[] vals)
        {
            var inverse = vals.Select(a => 1 - a).ToArray();
            List<double> res = new List<double>();

            for (int i = 0; i < vals.Length; i++)
            {
                if (i == 0) 
                {
                    res.Add(vals[0]);
                    continue;
                }

                var prob = inverse.Take(i).Aggregate(1.0, (result, element) => result * element)*vals[i];
                res.Add(prob);
            }

            return res.ToArray();

        }

    }
}
