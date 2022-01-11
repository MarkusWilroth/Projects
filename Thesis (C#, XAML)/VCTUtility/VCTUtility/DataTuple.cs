using System.Collections.Generic;

namespace VCTUtility
{
    public class DataTuple
    { //relevant träningsdata till Ann

        public int tagCount;
        public List<TrainingData> trainingList;

        public DataTuple(int tagCount, List<TrainingData> trainingList)
        {
            this.tagCount = tagCount;
            this.trainingList = trainingList;
        }
    }
}
