export const routeMenus = [
  {
    key: "1",
    // icon: () => h(PieChartOutlined),
    icon: "far fa-square-caret-right",
    // icon:  (pa:any)=>{
    //   console.log(pa);
    //   return vn1
    // },
    label: "Option 1",
    title: "Option 1",
  },
  {
    key: "2",
    icon: "fab fa-bandcamp",
    label: "Option 2",
    title: "Option 2",
  },
  {
    key: "3",
    icon: "far fa-square-caret-right",
    label: "Option 3",
    title: "Option 3",
  },
  {
    key: "aub1",
    icon: "far fa-square-caret-right",
    label: "Navigation One",
    title: "Navigation One",
    children: [
      {
        key: "a5",
        icon: "fas fa-plane-departure",
        label: "Option 5",
        title: "Option 5",
      },
      {
        key: "a6",
        label: "Option 6",
        title: "Option 6",
      },
    ],
  },
  {
    key: "bub1",
    icon: "far fa-square-caret-right",
    label: "Navigation One",
    title: "Navigation One",
    children: [
      {
        key: "b5",
        icon: "fas fa-plane-departure",
        label: "Option 5",
        title: "Option 5",
      },
      {
        key: "b6",
        label: "Option 6",
        title: "Option 6",
      },
    ],
  },
  {
    key: "cub1",
    icon: "far fa-square-caret-right",
    label: "Navigation One",
    title: "Navigation One",
    children: [
      {
        key: "c5",
        icon: "fas fa-plane-departure",
        label: "Option 5",
        title: "Option 5",
      },
      {
        key: "c6",
        label: "Option 6",
        title: "Option 6",
      },
    ],
  },
  {
    key: "dub1",
    icon: "far fa-square-caret-right",
    label: "Navigation One",
    title: "Navigation One",
    children: [
      {
        key: "d5",
        icon: "fas fa-plane-departure",
        label: "Option 5",
        title: "Option 5",
      },
      {
        key: "d6",
        label: "Option 6",
        title: "Option 6",
      },
    ],
  },
  {
    key: "sub1",
    icon: "far fa-square-caret-right",
    label: "Navigation One",
    title: "Navigation One",
    children: [
      {
        key: "5",
        icon: "fas fa-plane-departure",
        label: "Option 5",
        title: "Option 5",
      },
      {
        key: "6",
        label: "Option 6",
        title: "Option 6",
      },
      {
        key: "7",
        label: "Option 7",
        title: "Option 7",
      },
      {
        key: "8",
        label: "Option 8",
        title: "Option 8",
      },
    ],
  },
  {
    key: "sub2",
    icon: "fas fa-umbrella-beach",
    label: "Navigation Two",
    title: "Navigation Two",
    children: [
      {
        key: "9",
        label: "Option 9",
        title: "Option 9",
      },
      {
        key: "10",
        label: "Option 10",
        title: "Option 10",
      },
      {
        key: "sub3",
        label: "Submenu",
        title: "Submenu",
        children: [
          {
            key: "11",
            label: "Option 11",
            title: "Option 11",
          },
          {
            key: "12",
            label: "Option 12",
            title: "Option 12",
          },
        ],
      },
    ],
  },
  {
    key: "sub21",
    icon: "fas fa-cloud-sun",
    label: "Navigation Two",
    title: "Navigation Two",
    children: [
      {
        key: "91",
        label: "Option 9",
        title: "Option 9",
      },
      {
        key: "101",
        label: "Option 10",
        title: "Option 10",
      },
      {
        key: "sub31",
        label: "Submenu",
        title: "Submenu",
        children: [
          {
            key: "111",
            label: "Option 111",
            title: "Option 111",
          },
          {
            key: "121",
            label: "Option 121",
            title: "Option 121",
            children: [
              {
                key: "1111",
                label: "Option 111",
                title: "Option 111",
              },
              {
                key: "1211",
                label: "Option 121",
                title: "Option 121",
              },
            ],
          },
        ],
      },
    ],
  },
];

export default {};
