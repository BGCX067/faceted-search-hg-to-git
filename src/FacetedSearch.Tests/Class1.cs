using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using It= Machine.Specifications.It;

namespace FacetedSearch.Tests
{
    [Subject("Recent Account Activity Summary page")]
    public class when_a_customer_first_views_the_account_summary_page
    {
        It should_display_all_account_transactions_for_the_past_thirty_days;
        It should_display_debit_amounts_in_red_text;
        It should_display_deposit_amounts_in_black_text;
    }
}
