using CSharp.Japanese.Kanaxs;
using System;
using System.Globalization;

namespace JapaneseCalendarLibrary
{
    public class JapaneseCalendarLibrary
    {
        private readonly string[] ERA_TEXTS = { "dummy", "明治", "大正", "昭和", "平成", "令和" };
        /// <summary>
        /// 元号を表す数値。
        /// 1: 明治, 2: 大正, 3: 昭和, 4: 平成, 5:令和
        /// </summary>
        public int Era { get; private set; }
        /// <summary>
        /// 元号（日本語表記）
        /// </summary>
        public string EraText { get; private set; }
        /// <summary>
        /// 元号内での年。
        /// 例：平成３０年なら、30を返す
        /// </summary>
        public int YearInEra { get; private set; }
        /// <summary>
        /// 元号起点の年の文字表現。
        /// 例：平成1年なら1年を返す。ただし、コンストラクタのパラメータによっては、元年/１年となる。
        /// </summary>
        public string YearInEraText { get; private set; }
        /// <summary>
        /// 元号を利用して表した日付（日本語）。
        /// 例：平成1年1月31日。ただし、コンストラクタのパラメータによっては、平成元年1月31日/平成１年１月３１日となる。
        /// </summary>
        public string DateText { get; private set; }

        public JapaneseCalendarLibrary(DateTime originalDate, 
                                       bool treatFirstYearSpecially, bool makeNumbersFullWidth)
        {
            // originalDate is not null because it came from OutSystems as a Date Type value and it doesn't have null DateTime.
            // The minimum value of Date Type is #1900-01-01#
            var japaneseCalendar = new JapaneseCalendar();
            this.Era = japaneseCalendar.GetEra(originalDate);
            this.EraText = ERA_TEXTS[this.Era];
            this.YearInEra = japaneseCalendar.GetYear(originalDate);
            this.YearInEraText = (this.YearInEra == 1 && treatFirstYearSpecially ? "元年" : this.YearInEra + "年");
            if (makeNumbersFullWidth)
                this.YearInEraText = Kana.ToZenkaku(this.YearInEraText);
            this.DateText = $"{this.EraText}{this.YearInEraText}{originalDate.Month}月{originalDate.Day}日";
            if (makeNumbersFullWidth)
                this.DateText = Kana.ToZenkaku(this.DateText);
        }
    }
}
