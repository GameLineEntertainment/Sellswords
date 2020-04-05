﻿﻿using System;
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
                // TODO: FreezeBall
                case SpellType.HammerDash:
                    // TODO: HammerDash
                    break;
                case SpellType.FireTwister:
                    // TODO: FireTwister
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
                    // TODO: фриз
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}