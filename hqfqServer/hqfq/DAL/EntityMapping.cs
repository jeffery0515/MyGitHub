using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Mapping;
using Xktec.hqfq.Entity;

namespace DAL
{
    class LineInfoMap : ClassMap<LineInfo>
    {
        LineInfoMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Day);
            Map(x => x.OutCity);
            Map(x => x.IsShow);
            Map(x => x.IsRecommended);

            Map(x => x.AdWords);
            Map(x => x.IsPost);
           
            Map(x => x.PostTilte);
            Map(x => x.PostOrder);

            Map(x => x.ClickCount);
            Map(x => x.CreateTime);
            Map(x => x.Cautions);
            Map(x => x.ChargeExs);
            Map(x => x.ChargeIns);
            Map(x => x.Features);
            Map(x => x.SelfFincItems);
            Map(x => x.Tips);

            HasMany<Itinerary>(c => c.Itineraries).Cascade.All().KeyColumn("LineId");
            References<Category>(c => c.Category).Column("CategoryId");
            References<Image>(c => c.Image).Column("ImageId");
            References<Image>(c => c.PostImage).Column("PostImageId");

        }

    }
    class ItineraryMap : ClassMap<Itinerary>
    {
        ItineraryMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.OrderNum).Nullable();
            Map(x => x.MainContent);
            Map(x => x.Hotel);
            Map(x => x.HasBreakfast);
            Map(x => x.HasLunch);
            Map(x => x.HasDinner);
            References<LineInfo>(c => c.Line).Not.LazyLoad().Column("LineId");
        }
        class UserInfoMap : ClassMap<UserInfo>
        {
            UserInfoMap()
            {
                Id(x => x.Id);
                Map(x => x.LoginName).Unique();
                Map(x => x.Name).Unique();
                Map(x => x.Password);
            }
        }

        class CategoryMap : ClassMap<Category>
        {
            CategoryMap()
            {
                Id(x => x.Id);
                Map(x => x.Name);
                Map(x => x.Description);
                Map(x => x.CreateTime);

            }
        }
        class ImageMap : ClassMap<Image>
        {
            public ImageMap()
            {
                Id(x => x.Id);
                Map(x => x.Name);
                Map(x => x.Description);
                Map(x => x.CreateTime);
                Map(x => x.OriginalName);
                References<Category>(c => c.Category).Column("CategoryId");

            }
        }


    }
}
