using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemEF.Tests
{
    [TestFixture()]
    public class UdlejningsTest
    {
        [Test()]
        public void beregnFuldPrisTest1()
        {
            Værktøj værktøj = new Værktøj();
            værktøj.døgnpris = 100;
            værktøj.depositum = 50;

            Udlejning udlejning = new Udlejning();
            udlejning.FraDato = new DateTime(2021, 1, 1);
            udlejning.TilDato = new DateTime(2021, 1, 2);
            udlejning.Værktøj = værktøj;

            Assert.AreEqual(150, udlejning.beregnFuldPris());
        }

        [Test()]
        public void beregnFuldPrisTest2()
        {
            Værktøj værktøj = new Værktøj();
            værktøj.døgnpris = 200;
            værktøj.depositum = 100;

            Udlejning udlejning = new Udlejning();
            udlejning.FraDato = new DateTime(2021, 1, 5);
            udlejning.TilDato = new DateTime(2021, 1, 15);
            udlejning.Værktøj = værktøj;

            Assert.AreEqual(2100, udlejning.beregnFuldPris());
        }

        [Test()]
        public void beregnFuldPrisTest3()
        {
            Værktøj værktøj = new Værktøj();
            værktøj.døgnpris = 300;
            værktøj.depositum = 1000;

            Udlejning udlejning = new Udlejning();
            udlejning.FraDato = new DateTime(2021, 1, 28);
            udlejning.TilDato = new DateTime(2021, 2, 2);
            udlejning.Værktøj = værktøj;

            Assert.AreEqual(2500, udlejning.beregnFuldPris());
        }
    }
}