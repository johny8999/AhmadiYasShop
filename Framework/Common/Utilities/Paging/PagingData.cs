using System;

namespace Framework.Common.Utilities.Paging
{
    public static class PagingData
    {
        public static OutPagingData Calculate(long CountAllItem, int Page, int Take)
        {
            try
            {
                int _Skip = 0;
                int _CountPage = 5;
                int _CountAllPage = 0;


                Page = Page <= 0 ? 1 : Page;

                if (CountAllItem == 0)
                    return new OutPagingData()
                    {
                        CountAllItem = 0,
                        CountAllPage = 1,
                        Page = 1,
                        Take = Take,
                        Skip = 0,
                        StartPage = 1,
                        EndPage = 1
                    };

                _CountAllPage = (int)Math.Ceiling((decimal)CountAllItem / Take);
                Take = CountAllItem < Take ? (int)CountAllItem : Take;
                Page = _CountAllPage < Page ? _CountAllPage : Page;

                _Skip = (Take * Page) - Take;
                _Skip = _Skip < 0 ? 0 : _Skip;

                int _StartPage = (Page - _CountPage) <= 0 ? 1 : Page - _CountPage;
                int _EndPage = (Page + _CountPage) > _CountAllPage ? _CountAllPage : Page + _CountPage;

                return new OutPagingData()
                {
                    CountAllItem = CountAllItem,
                    CountAllPage = _CountAllPage,
                    Page = Page,
                    Take = Take,
                    Skip = _Skip,
                    StartPage = _StartPage,
                    EndPage = _EndPage
                };
            }
            catch
            {
                return new OutPagingData()
                {
                    CountAllItem = 0,
                    CountAllPage = 1,
                    Page = 1,
                    Take = Take,
                    Skip = 0,
                    StartPage = 1,
                    EndPage = 1
                };
            }
        }
    }
}
