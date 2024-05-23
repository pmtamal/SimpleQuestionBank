import { formatDate } from "../Utility/common";

const DateColumn = ({ dateStr }) => {
  let dateWithooutZoneStr = dateStr ? dateStr.replace("Z", "") : dateStr;
  const dateStrInTimeZone = dateWithooutZoneStr
    ? dateWithooutZoneStr + "Z"
    : undefined;

  return (
    <>
      {dateStrInTimeZone
        ? formatDate(new Date(dateStrInTimeZone), "YYYY/MM/DD HH:mm:SS")
        : ""}
    </>
  );
};

export default DateColumn;
