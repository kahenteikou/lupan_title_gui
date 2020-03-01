using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lupinmail_CS
{
    public class FFprobeJSON
    {
        public streams_class[] streams;
    }
    public class streams_class
    {
        public int index;
        public string codec_name;
        public string codec_long_name;
        public string profile;
        public string codec_type;
        public string codec_time_base;
        public string codec_tag_string;
        public string codec_tag;
        public int width;
        public int height;
        public int coded_width;
        public int coded_height;
        public int has_b_frames;
        public string pix_fmt;
        public int level;
        public string chroma_location;
        public int refs;
        public bool is_avc;
        public int nal_length_size;
        public string r_frame_rate;
        public string avg_frame_rate;
        public string time_base;
        public int start_pts;
        public double start_time;
        public int duration_ts;
        public double duration;
        public long bit_rate;
        public int bits_per_raw_sample;
        public long nb_frames;
        public tags_class tags;


    }
    public class tags_class
    {
        public string language;
        public string handler_name;
    }
}
