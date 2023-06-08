﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public sealed class FilterMatchAny : ItemFilter
{
#if USING_SUBCLASS_SELECTOR
    [SubclassSelector]
#endif
    [SerializeReference] private List<ItemFilter> criteria;

    public override bool Matches(ItemStack itemStack)
    {
        return criteria.Any(c => c.Matches(itemStack));
    }

    public override ItemFilter Clone()
    {
        return new FilterMatchAny()
        {
            criteria = criteria.Select(i => i.Clone()).ToList()
        };
    }
}