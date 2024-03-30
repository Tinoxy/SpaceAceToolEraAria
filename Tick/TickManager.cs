using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SpaceAceToolEraAria.Tick
{
    public class TickManager
        {
            private ConcurrentDictionary<Guid, ITick> _ticks { get; set; } = new();
            public int Count() => _ticks.Count;
            public void AddTick(ITick tick)
            {
                if(Exists(tick))
                {
                    _ticks.TryGetValue(tick.Id, out var existingTick);
                    RemoveTick(existingTick!.Id);
                }
                _ticks.TryAdd(tick.Id, tick);
                tick.OnRemove += Tick_OnRemove;
            }
            private void Tick_OnRemove(object? sender, ITick e)
            {
                RemoveTick(e.Id);
            }

            public void RemoveTick(Guid id)
            {
                if (_ticks.TryRemove(id, out var tick))
                {
                    tick.OnRemove -= Tick_OnRemove;
                }
            }
            public bool Exists(ITick tick) => _ticks.ContainsKey(tick.Id);
            private DateTime NextTick { get; set; } = DateTime.UtcNow;
            public async Task Tick()
            {
                var now = DateTime.UtcNow;
                var delay = NextTick - now;
                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay);
                }
                var ticksToProcess = _ticks.Values.Where(x => !x.Ticking).ToList();
                foreach (var tick in ticksToProcess)
                {
                    tick.Tick();
                }
                NextTick = DateTime.UtcNow.AddMilliseconds(20);
            }
        }
    }


