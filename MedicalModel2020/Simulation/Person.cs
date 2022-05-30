﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using Meta.Numerics;


public enum DeathStatus
{
    Natural,
    Cancer,
    NaturalSavedByScreening,
    NaturalCured,
    Alive,

}


namespace MedicalModel
{

    class BasePerson: ICloneable
    {

        public int DateBirth { set; get; }
        public bool IsAlive { set; get; }
        public int NaturalDeathAge { set; get; }
        public int CancerDeathAge { set; get; }
        public int DiagnosisAge { set; get; }
        public Cancer CurrentCancer { set; get; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int Age
        {
            get
            {
                return (int)(Environment.CurrentDate - this.DateBirth);
            }

        }


    }


    class Person : BasePerson, ICloneable
    {
        public long ID { set; get; }

        private Dictionary<string, Factor> factors = new Dictionary<string, Factor>();
        public Dictionary<string, Factor> Factors { get => factors; set => factors = value; }


        public int CancerDeathAgeNoScreening
        {
            get
            {
                if (CurrentCancer == null || CurrentCancer.CancerDeathAgeInit> NaturalDeathAge)
                {
                    return -1;
                }

                if (CurrentCancer.IsCured)
                {
                    return (int)CurrentCancer.CancerDeathAgeCure;
                }
                
                return (int)CurrentCancer.CancerDeathAgeInit;

            }

        }

        public int CancerDeathAgeScreening
        {
            get
            {
                if (CurrentCancer == null || CurrentCancer.CancerDeathAgeInit > NaturalDeathAge)
                {
                    return -1;
                }

                if (!CurrentCancer.ScreeningFound)
                {
                    return CancerDeathAgeNoScreening;
                }

                if (CurrentCancer.IsScreeningCured)
                {
                    return (int)CurrentCancer.CancerDeathAgeScreen;
                }

                return (int)CurrentCancer.CancerDeathAgeInit;

            }

        }


        public DeathStatus DeathCause
        {
            get
            {
                if (IsAlive && CurrentCancer is null)
                    return DeathStatus.Alive;

                if (CurrentCancer is null && NaturalDeathAge <= Age)
                    return DeathStatus.Natural;

                if (CurrentCancer != null
                    && NaturalDeathAge <= Age
                    && CancerDeathAge > NaturalDeathAge)
                {
                    return DeathStatus.Natural;
                }

                if (CurrentCancer != null
                    && NaturalDeathAge <= Age
                    && CancerDeathAge <= Age
                    && CurrentCancer.IsCured)
                {
                    return DeathStatus.NaturalCured;
                }

                if (CurrentCancer != null 
                    && NaturalDeathAge >= CancerDeathAge
                    && CancerDeathAge<=Age 
                    && !CurrentCancer.IsCured
                    && !CurrentCancer.IsScreeningCured)
                {
                    return DeathStatus.Cancer;
                }

                if (CurrentCancer != null
                    && NaturalDeathAge <= Age
                    && CancerDeathAge <= Age
                    && !CurrentCancer.IsCured
                    && CurrentCancer.IsScreeningCured)
                {
                    return DeathStatus.NaturalSavedByScreening;
                }

                return DeathStatus.Alive;
            }
        }

        public new object Clone()
        {
            return this.MemberwiseClone();
        }

        public Person(int _ID)
        {
            Environment.MaxID = _ID;
            ID = _ID;
            DateBirth = Environment.CurrentDate;
            IsAlive = true;
            CancerDeathAge = -1;
        }



        

    }
}

