export const formatDate = (dateObj, format) => {
  const pad = (num, size = 2) => num.toString().padStart(size, "0");

  const replacements = {
    YYYY: dateObj.getFullYear(),
    MM: pad(dateObj.getMonth() + 1),
    DD: pad(dateObj.getDate()),
    HH: pad(dateObj.getHours()),
    mm: pad(dateObj.getMinutes()),
    SS: pad(dateObj.getSeconds()),
    sss: pad(dateObj.getMilliseconds(), 3),
  };

  return format.replace(
    /YYYY|MM|DD|HH|mm|SS|sss/g,
    (match) => replacements[match]
  );
};

export const questionViewerType = {
  reviewer: 1,
  approver: 2,
  all: 3,
  creator: 4,
};
