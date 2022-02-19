export default {
  control: (provided) => ({
    ...provided,
    width: "100%",
    maxWidth: 300,
    border: "2px solid gray",
    borderRadius: 8,
    boxShadow: "none",
    background: "var(--background-input)",
    fontSize: 14,
  }),
  menu: (provided) => ({
    ...provided,
    width: "100%",
    maxWidth: 300,
    background: "var(--background)",
    color: "var(--foreground)",
  }),
  option: (provided, state) => ({
    ...provided,
    background: state.isSelected
      ? "var(--select-item-selected)"
      : state.isFocused
      ? "var(--select-item-focused)"
      : "none",
    ":active": {
      ...provided[":active"],
      background: "var(--select-item-focused)",
    },
  }),
  multiValue: (provided) => ({
    ...provided,
    background: "var(--select-value-background)",
    color: "var(--foreground)",
  }),
  multiValueLabel: (provided) => ({
    ...provided,
    color: "var(--foreground)",
  }),
  singleValue: (provided) => ({
    ...provided,
    color: "var(--foreground)",
  }),
  input: (provided) => ({
    ...provided,
    color: "var(--foreground)",
  }),
};
