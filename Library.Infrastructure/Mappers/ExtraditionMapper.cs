using Library.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Mappers
{
    public static class ExtraditionMapper
    {
        public static ExtraditionViewModel Map(ExtraditionEntity entity)
        {
            var viewModel = new ExtraditionViewModel
            {
                id = entity.id,
                data = entity.data,
                idbook = entity.idbook,
                idreaders = entity.idreaders,
            };
            return viewModel;
        }

        public static List<ExtraditionViewModel> Map(List<ExtraditionEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
        public static ExtraditionEntity Map(ExtraditionViewModel viewModel)
        {
            var entity = new ExtraditionEntity
            {
                id = viewModel.id,
                data = viewModel.data,
                idbook = viewModel.idbook,
                idreaders = viewModel.idreaders,
            };
            return entity;
        }
    }
}
