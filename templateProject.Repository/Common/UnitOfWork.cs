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
