using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseDetailsCreator<Accessories>
    {
        public AccessoriesCreator(int id, IMediator<Accessories> detailsMediator = null, CancellationToken token = default) :base(detailsMediator, token)
        {
            _id = id;
        }

        public override void Send()
        {
            var accessories = Create();
            Console.WriteLine($"AccessoriesCreator {_id}: Create Accessories-{accessories.Id}");
            _detailsMediator.Notify(CreatingStatus.Created, accessories,_id);
        }
        public override Accessories Create()
        {
            State = WorkState.Working;
            if (_token.IsCancellationRequested)
            {
                Console.WriteLine($"AccessoriesCreator Creator {_id} token.IsCancellationRequested");
                _token.ThrowIfCancellationRequested();
            }
            else
            {
                Thread.Sleep(Configure.AccessoriesCreateTime[_id]);
            }
            createdNumber++;
            State = WorkState.Waiting;
            return new Accessories();
        }
    }
}
