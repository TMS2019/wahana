using System;
using System.Linq;

using templateProject.Repository.Interface;

namespace templateProject.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Privates
        private Context Context;
        #endregion

        #region Constructors
        public UnitOfWork()
        {
            this.Context = new Context();
        }
        #endregion

        #region Methods
        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
                this.Context = null;
            }
        }

        private VoyageRepository Voyage;
        public VoyageRepository VoyageRepository
        {
            get
            {
                if (Voyage == null)
                {
                    Voyage = new VoyageRepository(Context);
                }
                return Voyage;
            }
        }

        private WageRepository Wage;
        public WageRepository WageRepository
        {
            get
            {
                if (Wage == null)
                {
                    Wage = new WageRepository(Context);
                }
                return Wage;
            }
        }

        private DummyRepository Dummy;
        public DummyRepository DummyRepository
        {
            get
            {
                if (Dummy == null)
                {
                    Dummy = new DummyRepository(Context);
                }
                return Dummy;
            }
        }
        #endregion

        #region Repos

         


        private MenuRepository repMenu;
        public MenuRepository MenuRepository
        {
            get
            {
                if (repMenu == null)
                {
                    repMenu = new MenuRepository(Context);
                }

                return repMenu;
            }
        }

        private VesselRepository repVessel;
        public VesselRepository VesselRepository
        {
            get
            {
                if (repVessel == null)
                {
                    repVessel = new VesselRepository(Context);
                }

                return repVessel;
            }
        }

         
        private PlanningRepository repPlanning;
        public PlanningRepository PlanningRepository
        {
            get
            {
                if(repPlanning==null)
                {
                    repPlanning = new PlanningRepository(Context);
                }
                return repPlanning;
            }
        }
        private UserRepository repUser;
        public UserRepository UserRepository
        {
            get
            {
                if (repUser == null)
                {
                    repUser = new UserRepository(Context);
                }

                return repUser;
            }
        }

        private DivisionRepository repDivision;
        public DivisionRepository DivisionRepository
        {
            get
            {
                if (repDivision == null)
                {
                    repDivision = new DivisionRepository(Context);
                }

                return DivisionRepository;
            }
        }
        private GroupUserMenuRepository repGroupUserMenu;
        public GroupUserMenuRepository GroupUserMenuRepository
        {
            get
            {
                if (repGroupUserMenu == null)
                {
                    repGroupUserMenu = new GroupUserMenuRepository(Context);
                }

                return repGroupUserMenu;
            }
        }
        //defact
        private DefactRepository DefRepo;
        public DefactRepository DefactRepository
        {
            get
            {
                if (DefRepo == null)
                {
                    DefRepo = new DefactRepository(Context);
                }

                return DefRepo;
            }
        }


        private PlantRepository plant;
        public PlantRepository PlantRepository
        {
            get
            {
                if (plant == null)
                {
                    plant = new PlantRepository(Context);
                }

                return plant;
            }
        }

        private ReadinessRepository read1;
        public ReadinessRepository ReadinessRepository
        {
            get
            {
                if (read1 == null)
                {
                    read1 = new ReadinessRepository(Context);
                }

                return read1;
            }
        }
        //read


        private GroupUserRepository repGroupUser;
        public GroupUserRepository GroupUserRepository
        {
            get
            {
                if (repGroupUser == null)
                {
                    repGroupUser = new GroupUserRepository(Context);
                }

                return repGroupUser;
            }
        }

        #endregion
    }
}
