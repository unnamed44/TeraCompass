﻿using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Tera.Core.Game.Messages.Server
{
    public class StartUserProjectileServerMessage : ParsedMessage
    {
        internal StartUserProjectileServerMessage(TeraMessageReader reader)
            : base(reader)
        {
            OwnerId = reader.ReadEntityId();
            reader.Skip(reader.Factory.ReleaseVersion>=7401?8:4);// something added
            Id = reader.ReadEntityId();
            SkillId = new SkillId(reader).Id;
            Start = reader.ReadVector3f();
            Finish = reader.ReadVector3f();
            Speed = reader.ReadSingle();
            //Trace.WriteLine($"{Time.Ticks} {BitConverter.ToString(BitConverter.GetBytes(Id.Id))} {Start} - > {Finish} {Speed} {ActionId}");
        }

        public float Speed { get; set; }
        public Vector3f Finish { get; set; }
        public Vector3f Start { get; set; }
        public int SkillId { get; set; }
        public EntityId Id { get; private set; }
        public EntityId OwnerId { get; private set; }
    }
}