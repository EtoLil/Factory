﻿using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseDetailsCreator<Engine>
    {
        public EngineCreator(int id, IMediator<Engine> detailsMediator = null) : base(detailsMediator)
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
            Thread.Sleep(Configure.EnginesCreateTime[_id]);
            return new Engine();
        }
    }
}
