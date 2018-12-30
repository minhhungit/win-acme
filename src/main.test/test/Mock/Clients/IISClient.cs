﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PKISharp.WACS.Clients.IIS;
using PKISharp.WACS.DomainObjects;

namespace PKISharp.WACS.UnitTests.Mock.Clients
{
    class MockIISClient : IIISClient<MockSite, MockBinding>
    {
        public Version Version { get => new Version(10, 0); }
        public IdnMapping IdnMapping => new IdnMapping();
        IEnumerable<IIISSite> IIISClient.FtpSites => FtpSites;
        IEnumerable<IIISSite> IIISClient.WebSites => WebSites;

        public IEnumerable<MockSite> FtpSites => throw new NotImplementedException();
        public IEnumerable<MockSite> WebSites => new[] {
            new MockSite()
            {
                Id = 1,
                Name = "Default Website",
                Path = "C:\\wwwroot",
                Bindings = new[]
                {
                    new MockBinding()
                    {
                        Host = "test.example.com",
                        Protocol = "http"
                    }
                }
            }
        };

        public bool HasFtpSites => FtpSites.Count() > 0;
        public bool HasWebSites => WebSites.Count() > 0;

        public void AddOrUpdateBindings(IEnumerable<string> identifiers, BindingOptions bindingOptions, byte[] oldThumbprint) { }
        public void Commit() { }
        public MockSite GetFtpSite(long id) => FtpSites.First(x => id == x.Id);
        public MockSite GetWebSite(long id) => WebSites.First(x => id == x.Id);
        public void UpdateFtpSite(long FtpSiteId, SSLFlags flags, CertificateInfo newCertificate, CertificateInfo oldCertificate) { }
        IIISSite IIISClient.GetFtpSite(long id) => GetFtpSite(id);
        IIISSite IIISClient.GetWebSite(long id) => GetWebSite(id);
    }

    class MockSite : IIISSite<MockBinding>
    {
        IEnumerable<IIISBinding> IIISSite.Bindings => Bindings;
        public IEnumerable<MockBinding> Bindings { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set;  }
    }

    class MockBinding : IIISBinding
    {
        public string Host { get; set; }
        public string Protocol { get; set; }
    }
}
