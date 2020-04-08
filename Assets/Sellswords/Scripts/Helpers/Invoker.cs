﻿using System;
using UnityEngine;

namespace Sellswords
{
    public class Invoker
    {
        public static Func<ISpell> CreateSpell(Vector3 poolPosition, SpellObject spellObject, UsableServices services)
        {
            Func<ISpell> result = null;

            switch (spellObject.SpellType)
            {
                case SpellType.None:
                    break;
                case SpellType.Meteor:
                    result = () => new SpellMeteor(poolPosition, spellObject, services);
                    break;
                case SpellType.Golem:
                    result = () => new SpellGolem(poolPosition, spellObject, services);
                    break;
                case SpellType.Shake:
                    result = () => new SpellShake(poolPosition, spellObject, services);
                    break;
                case SpellType.FreezeBall:
                    result = () => new SpellFreezeBall(poolPosition, spellObject, services);
                    break;
                case SpellType.HammerDash:
                    result = () => new SpellHammerDash(poolPosition, spellObject, services);
                    break;
                case SpellType.FireTwister:
                    result = () => new SpellFireTwist(poolPosition, spellObject, services);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public static Status CreateStatus(StatusObject statusObject)
        {
            Status result = null;

            switch (statusObject.StatusType)
            {
                case StatusType.None:
                    break;
                case StatusType.Slow:
                    result = new StatusSlow(statusObject);
                    break;
                case StatusType.Force:
                    result = new StatusForce(statusObject);
                    break;
                case StatusType.PhysicalDamage:
                    result = new StatusPhysicalDamage(statusObject);
                    break;
                case StatusType.Freeze:
                    result = new StatusFreeze(statusObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}