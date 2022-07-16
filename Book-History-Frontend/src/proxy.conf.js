const PROXY_CONFIG = [
  {
    context: [
      "/book",
    ],
    target: "https://localhost:7180",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
