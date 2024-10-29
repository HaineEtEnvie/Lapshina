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
                fullname = entity.fullname,
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
                fullname = viewModel.fullname,
                phonenumber = viewModel.phonenumber,
                adress = viewModel.adress,
            };
            return entity;
        }
    }
}
