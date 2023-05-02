using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class MemberBaseEntityWithTagConfiguration : BaseEntityWithTagConfiguration<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.Name).HasColumnType("nvarchar(50)");
        builder.Property(m => m.Role).HasColumnType("varchar(10)");

        builder.HasOne(cm => cm.League).WithMany();

        builder.Property(cm => cm.History)
            .HasColumnType("nvarchar(max)")
            .HasConversion(h => ToCsvString(h),
                s => FromCsvString(s),
                new ValueComparer<List<HistoryEvent>>(
                    (l1, l2) => l1 != null && l2 != null
                                           && l1.SequenceEqual(l2, new HistoryEventComparer()),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList())
            );
    }

    private static string ToCsvString(List<HistoryEvent> historyEvents)
    {
        //it may be larger than needed but we try to avoid new allocations
        var sb = new StringBuilder(historyEvents.Count * 100);
        foreach (var he in historyEvents)
        {
            sb.AppendLine($"{he.DateTime:yyyy-MM-DD HH:ss}\t{he.Property}\t{he.NewValue}\t{he.OldValue}");
        }

        return sb.ToString();
    }

    // yyyy-MM-DD HH:ss PropertyName NewValue OldValue

    private static List<HistoryEvent> FromCsvString(string s)
    {
        var result = new List<HistoryEvent>(s.Length / 50 + 2);
        using var reader = new System.IO.StringReader(s);
        while (reader.ReadLine() is { } line)
        {
            var l = line.Split('\t');
            if (!DateTime.TryParseExact(l[0], "yyyy-MM-DD HH:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out var dt)) continue;
            result.Add(new HistoryEvent(dt, l[1], l[2], l[3]));
        }

        return result;
    }

    private class HistoryEventComparer : IEqualityComparer<HistoryEvent>
    {
        public bool Equals(HistoryEvent? x, HistoryEvent? y)
        {
            if (x == null || y == null) return x == null && y == null;
            if (ReferenceEquals(x, y)) return true;
            if (x.GetType() != y.GetType()) return false;
            return x.DateTime.Equals(y.DateTime) && x.Property == y.Property;
        }

        public int GetHashCode(HistoryEvent obj) => HashCode.Combine(obj.DateTime, obj.Property);
    }
}