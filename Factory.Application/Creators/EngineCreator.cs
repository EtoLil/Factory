using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseDetailsCreator<Engine>
    {
        public EngineCreator(int id, IMediator<Engine> detailsMediator = null, CancellationToken token = default) : base(detailsMediator, token)
        {
            _id = id;
        }
        public override void Send()
        {
            var engine = Create();
            Console.WriteLine($"EngineCreator {_id}: Create Engine-{engine.Id}");
            _detailsMediator.Notify(CreatingStatus.Created, engine, _id);
        }
        public override Engine Create()
        {
            State = WorkState.Working;
            if (_token.IsCancellationRequested)
            {
                Console.WriteLine($"AccessoriesCreator Creator {_id} token.IsCancellationRequested");
                _token.ThrowIfCancellationRequested();
            }
            else
            {
                Thread.Sleep(Configure.EnginesCreateTime[_id]);
            }
            createdNumber++;
            State = WorkState.Waiting;
            return new Engine();
        }
    }
}
