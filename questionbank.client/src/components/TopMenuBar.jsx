import { Menubar } from "primereact/menubar";

const TopMenuBar = ({ startElement = null, items = [], endElement = null }) => {
  return <Menubar model={items} start={startElement} end={endElement} />;
};

export default TopMenuBar;
