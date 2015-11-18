// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using d3es.Sales.Domain.Vouchers;
using d3es.Framework.Bus;
using d3es.Framework.Commands;
using d3es.Framework.Domain.Persistence;
using d3es.Framework.Events;
using d3es.Framework.EventStore;
using d3es.Framework.EventStore.Snapshots;
using d3es.Framework.Projection;
using d3es.Sales.Infrastructure;
using d3es.WebUI.Models;
using StructureMap;
using StructureMap.Graph;
namespace d3es.WebUI.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            x.For<IBus>().Singleton().Use<InThreadBus>();
                            x.For<ICommandSender>().Use(y => y.GetInstance<IBus>());
                            x.For<IEventPublisher>().Use(y => y.GetInstance<IBus>());
                            x.For<IHandlerRegistrar>().Use(y => y.GetInstance<IBus>());
                            x.For<IEventStore>().Singleton().Use(y => y.GetInstance<InMemoryEventStore>());
                            x.For<IDocumentStore>().Singleton().Use(y => y.GetInstance<InMemoryDocumentStore>());
                            x.For<ISnapshotStore>().Singleton().Use(y => y.GetInstance<InMemorySnapshotStore>());
                            x.For<IVoucherCodeGenerator>().Use(y => y.GetInstance<VoucherCodeGenerator>());
                            x.For<IDocumentStrategy>().Use(y => y.GetInstance<DocumentStrategy>());
                            x.For<ISnapshotStrategy>().Use(y => y.GetInstance<DefaultSnapshotStrategy>());
                            x.For<IFacade>().Use(y => y.GetInstance<Facade>());

                            x.For<IRepository>().Use(y => new SnapshotRepository(y.GetInstance<ISnapshotStore>(),
                                                                                 y.GetInstance<ISnapshotStrategy>(),
                                                                                 y.GetInstance<IEventStore>(),
                                                                                 y.GetInstance<IEventPublisher>()));

                            x.For<ISession>().Use(y => new Session(y.GetInstance<IRepository>()));
                        });
            return ObjectFactory.Container;
        }
    }
}