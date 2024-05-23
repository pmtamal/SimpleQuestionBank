import { InputText } from "primereact/inputtext";

const InputFieldWithLabel = ({ labelText, onChangeHandler }) => {
  return (
    <div>
      <label>{labelText}:</label>

      <InputText
        value={value}
        onChange={(e) => setValue(e.target.value)}
        className="w-full"
      />
    </div>
  );
};

export default InputFieldWithLabel;
