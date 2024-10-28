using Library.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Mappers
{
    public static class ReadersMapper
    {
        public static ReadersViewModel Map(ReadersEntity entity)
        {
            var viewModel = new ReadersViewModel
            {
                id = entity.id,
                firstname = entity.firstname,
                secondname = entity.secondname,
                lastname = entity.lastname,
                phonenumber = entity.phonenumber,
                adress = entity.adress,
            };
            return viewModel;
        }

        public static List<ReadersViewModel> Map(List<ReadersEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
        public static ReadersEntity Map(ReadersViewModel viewModel)
        {
            var entity = new ReadersEntity
            {
                id = viewModel.id,
                firstname = viewModel.firstname,
                secondname = viewModel.secondname,
                lastname = viewModel.lastname,
                phonenumber = viewModel.phonenumber,
                adress = viewModel.adress,
            };
            return entity;
        }
    }
}
